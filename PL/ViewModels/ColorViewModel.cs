using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class ColorViewModel 
    {
        public Action CloseAction { get; set; }
        GIBDDContext db;
        //List<Model> Models;
        private Color current;
        public Color Current
        {
            get { return current; }
            set
            {
                current = value;
                OnPropertyChanged("Current");
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
                      db.Colors.Add(Current);
                      db.SaveChanges();
                      CloseAction();
                  }));
            }
        }

        public ColorViewModel(GIBDDContext db)
        {
            this.db = db;
            //Models = db.Models.ToList();
            Current = new Color();

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
