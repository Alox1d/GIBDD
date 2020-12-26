using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using PL.Views;

namespace PL.ViewModels
{
    class OffenseViewModel : INotifyPropertyChanged
    {


        OffenseService offenseService;

        private Offense selectedOffense;

        public Offense SelectedOffense
        {
            get { return selectedOffense; }
            set
            {
                selectedOffense = value;

                OnPropertyChanged("SelectedOffense");
            }
        }
        private VehicleOffense selectedVehicleOffense;

        public VehicleOffense SelectedVehicleOffense
        {
            get { return selectedVehicleOffense; }
            set
            {
                selectedVehicleOffense = value;
                OnPropertyChanged("SelectedVehicleOffense");
            }
        }

        private bool isTakeLicense;

        public bool IsTakeLicense
        {
            get { return isTakeLicense; }
            set
            {
                isTakeLicense = value;
                if (isTakeLicense)
                {
                    SelectedTakeStroke = new TakeStroke { ReturnDate = DateTime.Now };

                }

                else
                    SelectedTakeStroke = null;
                OnPropertyChanged("IsTakeLicense");
            }
        }

        private Vehicle selectedVehicle;

        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                OnPropertyChanged("SelectedVehicle");
            }
        }
        private TakeStroke selectTakeStroke;

        public TakeStroke SelectedTakeStroke
        {
            get { return selectTakeStroke; }
            set
            {
                selectTakeStroke = value;
                OnPropertyChanged("SelectedTakeStroke");
            }
        }
        private GIBDDContext db;

        public ObservableCollection<Offense> Offenses { get; set; }
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ApplicationViewModel ApplicationViewModel { get; set; }


        public OffenseViewModel(ApplicationViewModel ApplicationViewModel)
        {
            db = new GIBDDContext();
            this.ApplicationViewModel = ApplicationViewModel;
            this.offenseService = new OffenseService();
            LoadData();
        }
        private void LoadData()
        {

            Offenses = new ObservableCollection<Offense>(db.Offenses.ToList());
            Vehicles = new ObservableCollection<Vehicle>(db.Vehicles.ToList());
            SelectedOffense = null;
            ApplicationViewModel.DriverLicenseViewModel = new DriverLicenseViewModel();
            SelectedTakeStroke = null;

        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Offense v = new Offense();
                      Offenses.Insert(0, v);
                      //SelectedCarDriver = 
                      var carDriver = new CarDriver();
                      carDriver.VehicleOffenses = new ObservableCollection<VehicleOffense>();
                      v.CarDriver = carDriver;
                      SelectedOffense = v;

                  }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      offenseService.createOffense(SelectedOffense,SelectedTakeStroke);
                      if (SelectedOffense != null && SelectedOffense.Id == 0)
                      {
                          db.Offenses.Add(SelectedOffense);
                      }
                      db.SaveChanges();
                      LoadData();

                  }
                 ));
            }
        }

        private void createOffense()
        {
            if (SelectedOffense != null)
                if (SelectedOffense.CarDriver.Vehicle.CarOwner != null && SelectedOffense.CarDriver.IsCarOwner)
                {
                    SelectedOffense.CarDriver.FIO = SelectedOffense.CarDriver.Vehicle.CarOwner.FIO;
                    if (SelectedOffense.CarDriver.Vehicle.CarOwner.DriverLicense != null)
                    {
                        double sum = 0;
                        foreach (var VO in SelectedOffense.CarDriver.VehicleOffenses)
                        {
                            sum += VO.Penalty;
                        }
                        SelectedOffense.SumPenalty = sum;
                        //TakeStroke takeStroke = new TakeStroke { TakeDate = DateTime.Now, ReturnDate = DateTime.Now.AddMonths(sum) };
                        if (SelectedTakeStroke != null)
                        {
                            if ((SelectedTakeStroke.ReturnDate - DateTime.Now).TotalSeconds > 0)
                            {
                                SelectedOffense.CarDriver.Vehicle.CarOwner.DriverLicense.IsInvalid = true;
                            }
                            SelectedTakeStroke.TakeDate = SelectedOffense.Date;
                            SelectedOffense.CarDriver.Vehicle.CarOwner.DriverLicense.TakeStrokes.Add(SelectedTakeStroke);
                        }

                    }
                }
            if (SelectedOffense != null && SelectedOffense.Id == 0)
            {
                db.Offenses.Add(SelectedOffense);
            }
        }



        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Offense v = obj as Offense;
                      if (v != null)
                      {
                          Offenses.Remove(v);
                          if (v.Id != 0)
                              db.Offenses.Remove(v);
                      }
                  },
                 (obj) => Offenses.Count > 0));
            }
        }

        private RelayCommand deleteVehicleOffenseCommand;
        public RelayCommand DeleteVehicleOffenseCommand
        {
            get
            {
                return deleteVehicleOffenseCommand ??
                  (deleteVehicleOffenseCommand = new RelayCommand(obj =>
                  {
                      VehicleOffense v = obj as VehicleOffense;
                      if (v != null)
                      {
                          SelectedOffense.CarDriver.VehicleOffenses.Remove(v);
                          db.CarDrivers.Find(SelectedOffense.CarDriver.Id).VehicleOffenses.Remove(v);
                      }
                  },
                 (obj) => SelectedOffense != null
                 && SelectedOffense.CarDriver != null
                 && SelectedOffense.CarDriver.VehicleOffenses != null
                 && SelectedOffense.CarDriver.VehicleOffenses.Count > 0));
            }
        }
        private RelayCommand addVehicleOffenseCommand;
        public RelayCommand AddVehicleOffenseCommand
        {
            get
            {
                return addVehicleOffenseCommand ??
                  (addVehicleOffenseCommand = new RelayCommand(obj =>
                  {
                      AddVehicleOffense w = new AddVehicleOffense();
                      ArticleOffenseViewModel m = new ArticleOffenseViewModel(db);
                      m.CloseAction = new Action(w.Close);
                      VehicleOffense vehicleOffense = new VehicleOffense();
                      m.SelectedVehicleOffense = vehicleOffense;
                      w.DataContext = m;
                      w.ShowDialog();
                      if (SelectedOffense != null && vehicleOffense != null)
                          SelectedOffense.CarDriver.VehicleOffenses.Add(vehicleOffense);
                      //LoadData();
                      double sum = 0;
                      foreach (var VO in SelectedOffense.CarDriver.VehicleOffenses)
                      {
                          sum += VO.Penalty;
                      }
                      SelectedOffense.SumPenalty = sum;
                      OnPropertyChanged("SelectedOffense");
                  }));
            }
        }
        #region prop
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
