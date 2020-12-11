using GIBDD;
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
using VehicleModel = PL.Models.VehicleModel;

namespace PL.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
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
        private Model selectedModel;
        private GIBDDContext db;
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<Color> Colors { get; set; }
        public ObservableCollection<Model> Models { get; set; }
        public int test { get; set; }
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
                  }));
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveChangesCommand { get; set; }

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
                      }
                  },
                 (obj) => Vehicles.Count > 0));
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      db.SaveChanges();
                  }
                 ));
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

        public ApplicationViewModel()
        {
            db = new GIBDDContext();
            //Vehicles = new ObservableCollection<Vehicle>
            //{
            //    new Vehicle { RegistrationNumber="А707ХХ" }
            //};
            var vehicles = db.Vehicles.ToList();
            Vehicles = new ObservableCollection<Vehicle>(vehicles);
            Colors = new ObservableCollection<Color>(db.Colors.ToList());
            Models = new ObservableCollection<Model>(db.Models.ToList());

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
