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
    class ColorViewModel 
    {
        private Color selectedColor;

        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                selectedColor = value;
                //selectedVehicleOffense.ArticleOffense = SelectedArticleOffense;
                OnPropertyChanged("SelectedColor");
            }
        }

        public ObservableCollection<Color> Colors { get; set; }

        private GIBDDContext db;
        public Action CloseAction { get; set; }

        public ColorViewModel(GIBDDContext db)
        {
            this.db = db;
            LoadData();

        }
        private void LoadData()
        {

            Colors = new ObservableCollection<Color>(db.Colors.ToList());

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
                      Color v = new Color();
                      Colors.Insert(0, v);
                      SelectedColor = v;
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
                      if (SelectedColor != null && SelectedColor.Id == 0)
                      {
                          db.Colors.Add(SelectedColor);
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
                      Color v = obj as Color;
                      if (v != null)
                      {
                          Colors.Remove(v);
                          if (v.Id != 0)
                              db.Colors.Remove(v);
                      }
                  },
                 (obj) => Colors.Count > 0));
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
