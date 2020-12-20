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
    class ArticleOffenseViewModel
    {
        //MaintenanceService maintenanceService;

        private ArticleOffense selectedArticleOffense;

        public ArticleOffense SelectedArticleOffense
        {
            get { return selectedArticleOffense; }
            set
            {
                selectedArticleOffense = value;
                SelectedVehicleOffense.ArticleOffense = SelectedArticleOffense;
                OnPropertyChanged("SelectedArticleOffense");
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

        public ObservableCollection<ArticleOffense> ArticleOffenses { get; set; }

        private GIBDDContext db;
        public Action CloseAction { get; set; }

        public ArticleOffenseViewModel(GIBDDContext db)
        {
            this.db = db;
            SelectedVehicleOffense = new VehicleOffense();
            LoadData();

        }
        private void LoadData()
        {

            ArticleOffenses = new ObservableCollection<ArticleOffense>(db.ArticleOffenses.ToList());

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
                      ArticleOffense v = new ArticleOffense();
                      ArticleOffenses.Insert(0, v);
                      SelectedArticleOffense = v;
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
                      if (SelectedArticleOffense != null && SelectedArticleOffense.Id == 0)
                      {
                          db.ArticleOffenses.Add(SelectedArticleOffense);
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
                      ArticleOffense v = obj as ArticleOffense;
                      if (v != null)
                      {
                          ArticleOffenses.Remove(v);
                          if (v.Id != 0)
                              db.ArticleOffenses.Remove(v);
                      }
                  },
                 (obj) => ArticleOffenses.Count > 0));
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
