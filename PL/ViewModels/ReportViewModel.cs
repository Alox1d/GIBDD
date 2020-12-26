using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bll.Models.ReportsModel;
using BLL.Services;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using PL.Views;

namespace PL.ViewModels
{
    class ReportViewModel : INotifyPropertyChanged
    {
        ReportsService reportsService;

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        private GIBDDContext db;

        public ObservableCollection<ArticleSum> ArticlesSum { get; set; }
        public ObservableCollection<CarOwner> CarOwners { get; set; }


        public ReportViewModel()
        {
            db = new GIBDDContext();
            this.reportsService = new ReportsService();
            LoadData();


        }
        private void LoadData()
        {
            EndDate = DateTime.Now;
            StartDate = DateTime.Parse("1/1/2000");
            ArticlesSum = new ObservableCollection<ArticleSum>(
                reportsService.MakeReport(db.VehicleOffenses.ToList(), StartDate, EndDate)
                );
        }

        private RelayCommand makeReport;
        //public RelayCommand CheckServeCommand { get; set; }
        public RelayCommand MakeReport
        {
            get
            {
                return makeReport ??
                  (makeReport = new RelayCommand(obj =>
                  { 
                      //reportsService.MakeReport(db.Offenses.ToList(), StartDate, EndDate);
                      LoadData();

                      //Message w = new Message();
                      //MessageViewModel m = new MessageViewModel("Проверено прав:", count.ToString());
                      //w.DataContext = m;
                      //w.ShowDialog();
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
