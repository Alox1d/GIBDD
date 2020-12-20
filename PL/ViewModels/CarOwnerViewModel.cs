using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;

namespace PL.ViewModels
{
    class CarOwnerViewModel : INotifyPropertyChanged
    {

        //MaintenanceService maintenanceService;

        private CarOwner selectedCarOwner;

        public CarOwner SelectedCarOwner
        {
            get { return selectedCarOwner; }
            set
            {
                selectedCarOwner = value;
                OnPropertyChanged("SelectedCarOwner");
            }
        }
        private Vehicle selectedVehicle;

        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                //SelectedCarOwner.Vehicles.Add(SelectedVehicle);

                OnPropertyChanged("SelectedVehicle");
            }
        }

        public ObservableCollection<CarOwner> CarOwners { get; set; }
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        private GIBDDContext db;

        public CarOwnerViewModel()
        {
            db = new GIBDDContext();

            LoadData();

            //this.maintenanceService = new MaintenanceService(db);

        }
        private void LoadData()
        {

            CarOwners = new ObservableCollection<CarOwner>(db.CarOwners.ToList());
            Vehicles = new ObservableCollection<Vehicle>(db.Vehicles.ToList());

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
                      CarOwner v = new CarOwner();
                      CarOwners.Insert(0, v);
                      SelectedCarOwner = v;

                  }));
            }
        }
        private RelayCommand addExistingVehicle;
        public RelayCommand AddExistingVehicle
        {
            get
            {
                return addExistingVehicle ??
                  (addExistingVehicle = new RelayCommand(obj =>
                  {
                      if (SelectedVehicle != null)
                      SelectedCarOwner.Vehicles.Add(SelectedVehicle);

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
                      if (SelectedCarOwner != null && SelectedCarOwner.Id == 0)
                      {
                          db.CarOwners.Add(SelectedCarOwner);
                      }
                      db.SaveChanges();
                      LoadData();
                  }
                 ));
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
                      CarOwner v = obj as CarOwner;
                      if (v != null)
                      {
                          CarOwners.Remove(v);
                          if (v.Id != 0)

                              db.CarOwners.Remove(v);
                      }
                  },
                 (obj) => CarOwners.Count > 0));
            }
        }
        private RelayCommand deleteVehicleCommand;
        public RelayCommand DeleteVehicleCommand
        {
            get
            {
                return deleteVehicleCommand ??
                  (deleteVehicleCommand = new RelayCommand(obj =>
                  {
                      Vehicle v = obj as Vehicle;
                      if (v != null)
                      {
                          SelectedCarOwner.Vehicles.Remove(v);
                          db.CarOwners.Find(SelectedCarOwner.Id).Vehicles.Remove(v);
                      }
                  },
                 (obj) => SelectedCarOwner != null && SelectedCarOwner.Vehicles != null && SelectedCarOwner.Vehicles.Count > 0));
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
