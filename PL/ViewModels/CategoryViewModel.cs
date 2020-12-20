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
    class CategoryViewModel
    {

        private Category selectedCategory;

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                //selectedVehicleOffense.ArticleOffense = SelectedArticleOffense;
                OnPropertyChanged("SelectedCategory");
            }
        }
        //private VehicleOffense selectedVehicleOffense;

        //public VehicleOffense SelectedVehicleOffense
        //{
        //    get { return selectedVehicleOffense; }
        //    set
        //    {
        //        selectedVehicleOffense = value;
        //        OnPropertyChanged("SelectedVehicleOffense");
        //    }
        //}

        public ObservableCollection<Category> Categories { get; set; }

        private GIBDDContext db;
        public Action CloseAction { get; set; }

        public CategoryViewModel(GIBDDContext db)
        {
            this.db = db;
            //SelectedVehicleOffense = new VehicleOffense();
            LoadData();

        }
        private void LoadData()
        {

            Categories = new ObservableCollection<Category>(db.Categories.ToList());

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
                      Category v = new Category();
                      Categories.Insert(0, v);
                      SelectedCategory = v;
                  }));
            }
        }
        // команда добавления нового объекта
        private RelayCommand addVOCommand;
        public RelayCommand AddVOCommand
        {
            get
            {
                return addVOCommand ??
                  (addVOCommand = new RelayCommand(obj =>
                  {
                      //return SelectedArticleOffense
                      CloseAction();
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
                      if (SelectedCategory != null && SelectedCategory.Id == 0)
                      {
                          db.Categories.Add(SelectedCategory);
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
                      Category v = obj as Category;
                      if (v != null)
                      {
                          Categories.Remove(v);
                          if (v.Id != 0)
                              db.Categories.Remove(v);
                      }
                  },
                 (obj) => Categories.Count > 0));
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
