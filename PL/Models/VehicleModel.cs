using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class VehicleModel : INotifyPropertyChanged
    {
        private string registrationNumber;
        private string milliage;
        private string model;
        private string color;
        private DateTime maintenanceDate;
        private bool maintenanceSuccess;
        private string carOwner;
        private string driverLicense;

        public VehicleModel(Vehicle v)
        {
            //RegistrationNumber = v.RegistrationNumber;
        }
        public VehicleModel()
        {
        }

        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set
            {
                registrationNumber = value;
                OnPropertyChanged();
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }
        public DateTime MaintenanceDate
        {
            get { return maintenanceDate; }
            set
            {
                maintenanceDate = value;
                OnPropertyChanged();
            }
        }
        public bool MaintenanceSuccess
        {
            get { return maintenanceSuccess; }
            set
            {
                maintenanceSuccess = value;
                OnPropertyChanged();
            }
        }
        public string CarOwner
        {
            get { return carOwner; }
            set
            {
                carOwner = value;
                OnPropertyChanged();
            }
        }
        public string DriverLicense
        {
            get { return driverLicense; }
            set
            {
                driverLicense = value;
                OnPropertyChanged();
            }
        }
        //public string Mileage
        //{
        //    get { return milliage; }
        //    set
        //    {
        //        milliage = value;
        //        OnPropertyChanged();
        //    }
        //}
        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
