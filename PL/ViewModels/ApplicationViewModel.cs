using BLL.Services;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using PL.Models;
using PL.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VehicleModel = PL.Models.VehicleModel;
using System.Data.Entity;
namespace PL.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        OffenseViewModel offenseViewModel;

        public OffenseViewModel OffenseViewModel
        {
            get { return offenseViewModel; }
            set
            {
                offenseViewModel = value;
                OnPropertyChanged("OffenseViewModel");
            }
        }
        DriverLicenseViewModel driverLicenseViewModel;

        public DriverLicenseViewModel DriverLicenseViewModel
        {
            get { return driverLicenseViewModel; }
            set
            {
                driverLicenseViewModel = value;
                OnPropertyChanged("DriverLicenseViewModel");
            }
        }
        CarOwnerViewModel carOwnerViewModel;

        public CarOwnerViewModel CarOwnerViewModel
        {
            get { return carOwnerViewModel; }
            set
            {
                carOwnerViewModel = value;
                OnPropertyChanged("CarOwnerViewModel");
            }
        }
        MaintenanceService maintenanceService;

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
        private GIBDDContext db;
        private IDbRepos repos;
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<Color> Colors { get; set; }
        public ObservableCollection<Model> Models { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public ApplicationViewModel()
        {
            db = new GIBDDContext();
            repos = new DBReposSQL();
            //Vehicles = new ObservableCollection<Vehicle>
            //{
            //    new Vehicle { RegistrationNumber="А707ХХ" }
            //};
            LoadData();

            this.maintenanceService = new MaintenanceService(db);




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
                      Vehicle v = new Vehicle();
                      Vehicles.Insert(0, v);
                      SelectedVehicle = v;

                  }));
            }
        }
        private RelayCommand addModelCommand;
        public RelayCommand AddModelCommand
        {
            get
            {
                return addModelCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      AddModel w = new AddModel();
                      ModelViewModel m = new ModelViewModel(db);
                      m.CloseAction = new Action(w.Close);
                      w.DataContext = m;
                      w.ShowDialog();
                      LoadData();
                  }));
            }
        }
        private RelayCommand addColorCommand;
        public RelayCommand AddColorCommand
        {
            get
            {
                return addColorCommand ??
                  (addColorCommand = new RelayCommand(obj =>
                  {
                      AddColor w = new AddColor();
                      ColorViewModel m = new ColorViewModel(db);
                      m.CloseAction = new Action(w.Close);
                      w.DataContext = m;
                      //w.DataContext = new ColorViewModel(db);
                      w.ShowDialog();
                      LoadData();

                  }));
            }
        }
        private RelayCommand addCategoryCommand;
        public RelayCommand AddCategoryCommand
        {
            get
            {
                return addCategoryCommand ??
                  (addCategoryCommand = new RelayCommand(obj =>
                   {
                      AddCategory w = new AddCategory();
                      CategoryViewModel m = new CategoryViewModel(db);
                      m.CloseAction = new Action(w.Close);
                      //VehicleOffense vehicleOffense = new VehicleOffense();
                      //m.SelectedVehicleOffense = vehicleOffense;
                      w.DataContext = m;
                      w.ShowDialog();
                      if (m.SelectedCategory != null)
                          SelectedVehicle.Category = m.SelectedCategory;
                      //SelectedOffense.CarDriver.VehicleOffenses.Add(vehicleOffense);
                      //LoadData();

                  }));
            }
        }
        private RelayCommand saveCommand;
        //public RelayCommand SaveChangesCommand { get; set; }
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      if (SelectedVehicle != null && SelectedVehicle.Id == 0)
                      {
                          db.Vehicles.Add(SelectedVehicle);
                      }
                      db.SaveChanges();
                      LoadData();
                  }
                 ));
            }
        }

        private RelayCommand checkServe;
        //public RelayCommand CheckServeCommand { get; set; }
        public RelayCommand CheckServe
        {
            get
            {
                return checkServe ??
                  (checkServe = new RelayCommand(obj =>
                  {
                      int count = maintenanceService.CheckMaintenance(Vehicles.ToList());
                      LoadData();
                      //db.SaveChanges();
                      SelectedVehicle = null;

                      Message w = new Message();
                      MessageViewModel m = new MessageViewModel("Обновлено ТО", count.ToString());
                      //m.CloseAction = new Action(w.Close);
                      w.DataContext = m;
                      //w.DataContext = new ColorViewModel(db);
                      w.ShowDialog();
                      //MessageBox.Show("Кол-во обновленных ТО: " + count);
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
                      Vehicle v = obj as Vehicle;
                      if (v != null)
                      {
                          Vehicles.Remove(v);
                          if (v.Id != 0)
                          {
                              db.CarDrivers.Where(c => c.Vehicle.Id == v.Id).Load();
                              db.Vehicles.Remove(v);
                          }
                      }
                  },
                 (obj) => Vehicles.Count > 0));
            }
        }

        private RelayCommand doubleCommand;
        //public RelayCommand DoubleCommand
        //{
        //    get
        //    {
        //        return doubleCommand ??
        //            (doubleCommand = new RelayCommand(obj =>
        //            {
        //                Vehicle vehicle = obj as Vehicle;
        //                if (vehicle != null)
        //                {
        //                    Vehicle vcopy = new Vehicle
        //                    {
        //                        Company = vehicle.Company,
        //                        Price = vehicle.Price,
        //                        Title = vehicle.Title
        //                    };
        //                    Vehicles.Insert(0, vcopy);
        //                }
        //            }));
        //    }
        //}
        private Model selectedModel;

        public Model SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                SelectedVehicle.Model = selectedModel;

                OnPropertyChanged("SelectedModel");
                SelectedVehicle.Model = selectedModel;
            }
        }



        private void LoadData()
        {
            //db.Vehicles.Include(p=>p.Ca);
            var vehicles = db.Vehicles.ToList();
            Vehicles = new ObservableCollection<Vehicle>(vehicles);
            Colors = new ObservableCollection<Color>(db.Colors.ToList());
            Models = new ObservableCollection<Model>(db.Models.ToList());
            Categories = new ObservableCollection<Category>(db.Categories.ToList());

            OffenseViewModel = new OffenseViewModel(this);
            CarOwnerViewModel = new CarOwnerViewModel();
            DriverLicenseViewModel = new DriverLicenseViewModel();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
