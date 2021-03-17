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
    class DriverLicenseViewModel : INotifyPropertyChanged
    {
        DriverLicenseService driverLicenseService;

        private DriverLicense selectedDriverLicense;

        public DriverLicense SelectedDriverLicense
        {
            get { return selectedDriverLicense; }
            set
            {
                selectedDriverLicense = value;
                OnPropertyChanged("SelectedDriverLicense");
            }
        }
        private TakeStroke selectedTakeStroke;

        public TakeStroke SelectedTakeStroke
        {
            get { return selectedTakeStroke; }
            set
            {
                selectedTakeStroke = value;
                //selectedTakeStroke.ReturnDate = 
                OnPropertyChanged("SelectedTakeStroke");
            }
        }

        private Category selectedCategory;

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        private GIBDDContext db;

        public ObservableCollection<DriverLicense> DriverLicenses { get; set; }
        public ObservableCollection<CarOwner> CarOwners { get; set; }


        public DriverLicenseViewModel()
        {
            db = new GIBDDContext();

            LoadData();

            this.driverLicenseService = new DriverLicenseService(  );

        }
        private void LoadData()
        {

            DriverLicenses = new ObservableCollection<DriverLicense>(db.DriverLicenses.ToList());
            CarOwners = new ObservableCollection<CarOwner>(db.CarOwners.ToList());
            SelectedDriverLicense = null;
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
                      DriverLicense v = new DriverLicense();
                      v.Categories = new ObservableCollection<Category>();
                      DriverLicenses.Insert(0, v);
                      //v.CarDriver = new CarDriver();
                      //SelectedOffense = v;

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
                      if (SelectedDriverLicense != null && SelectedDriverLicense.Id == 0)
                      {
                          db.DriverLicenses.Add(SelectedDriverLicense);
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
                      DriverLicense v = obj as DriverLicense;
                      if (v != null)
                      {
                          DriverLicenses.Remove(v);
                          if (v.Id != 0)
                              db.DriverLicenses.Remove(v);
                      }
                  },
                 (obj) => DriverLicenses.Count > 0));
            }
        }

        private RelayCommand deleteTakeStrokeCommand;
        public RelayCommand DeleteTakeStrokeCommand
        {
            get
            {
                return deleteTakeStrokeCommand ??
                  (deleteTakeStrokeCommand = new RelayCommand(obj =>
                  {
                      TakeStroke v = obj as TakeStroke;
                      if (v != null)
                      {
                          SelectedDriverLicense.TakeStrokes.Remove(v);
                          db.DriverLicenses.Find(SelectedDriverLicense.Id).TakeStrokes.Remove(v);
                      }
                  },
                 (obj) => SelectedDriverLicense != null
                 && SelectedDriverLicense.TakeStrokes != null
                 && SelectedDriverLicense.TakeStrokes.Count > 0));
            }
        }
        private RelayCommand deleteCategoryCommand;
        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                return deleteCategoryCommand ??
                  (deleteCategoryCommand = new RelayCommand(obj =>
                  {
                      Category v = obj as Category;
                      if (v != null)
                      {
                          SelectedDriverLicense.Categories.Remove(v);
                          db.DriverLicenses.Find(SelectedDriverLicense.Id).Categories.Remove(v);
                      }
                  },
                 (obj) => SelectedDriverLicense != null
                 && SelectedDriverLicense.Categories != null
                 && SelectedDriverLicense.Categories.Count > 0));
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
                      if (SelectedDriverLicense != null && m.SelectedCategory != null)
                          SelectedDriverLicense.Categories.Add(m.SelectedCategory);
                          //SelectedOffense.CarDriver.VehicleOffenses.Add(vehicleOffense);
                      //LoadData();

                  }));
            }
        }
        private RelayCommand checkLicense;
        //public RelayCommand CheckServeCommand { get; set; }
        public RelayCommand CheckLicense
        {
            get
            {
                return checkLicense ??
                  (checkLicense = new RelayCommand(obj =>
                  {
                      int count = driverLicenseService.CheckLicense(db.DriverLicenses.ToList());
                      db.SaveChanges();
                      LoadData();

                      Message w = new Message();
                      MessageViewModel m = new MessageViewModel("Проверено прав:", count.ToString());
                      w.DataContext = m;
                      w.ShowDialog();



                      //MessageBox.Show("Кол-во обновленных ТО: " + count);
                  }
                 ));
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
