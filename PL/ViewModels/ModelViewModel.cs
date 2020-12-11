using GIBDD;
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
        public Action CloseAction { get; set; }
        GIBDDContext db;
        //List<Model> Models;
        private Model currentModel;
        public Model CurrentModel
        {
            get { return currentModel; }
            set
            {
                currentModel = value;
                OnPropertyChanged("CurrentModel");
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      //Model v = new Model();
                      db.Models.Add(CurrentModel);
                      db.SaveChanges();
                      CloseAction();

                  }));
            }
        }

        public ModelViewModel(GIBDDContext db)
        {
            this.db = db;
            //Models = db.Models.ToList();
            CurrentModel = new Model();

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
