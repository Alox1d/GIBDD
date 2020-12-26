using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class ModelViewModel : INotifyPropertyChanged
    {
        private Model selectedModel;

        public Model SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }

        public ObservableCollection<Model> Models { get; set; }

        private GIBDDContext db;
        public Action CloseAction { get; set; }

        public ModelViewModel(GIBDDContext db)
        {
            this.db = db;
            LoadData();

        }
        private void LoadData()
        {

            Models = new ObservableCollection<Model>(db.Models.ToList());

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
                      Model v = new Model();
                      Models.Insert(0, v);
                      SelectedModel = v;
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
                      if (SelectedModel != null && SelectedModel.Id == 0)
                      {
                          db.Models.Add(SelectedModel);
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
                      Model v = obj as Model;
                      if (v != null)
                      {
                          Models.Remove(v);
                          if (v.Id != 0)
                              db.Models.Remove(v);
                      }
                  },
                 (obj) => Models.Count > 0));
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
