using GIBDD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public int? Mileage { get; set; } // Пробег
        public Model Model { get; set; } 

        public Color Color { get; set; }

        public DateTime? MaintenanceDate { get; set; }
        public bool MaintenanceSuccess { get; set; }

        public CarOwner CarOwner { get; set; }
        //public CarDriver CarDriver { get; set; }
        public DriverLicense DriverLicense { get; set; }




    }
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vehicle>? Vehicles { get; set; }



    }

    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }


    }
}
