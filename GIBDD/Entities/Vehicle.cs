using GIBDD.Entities;
using PostSharp.Patterns.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD
{
    public class Vehicle
    {
        public int Id { get; set; }
         
        public string RegistrationNumber { get; set; }
        //public int? Mileage { get; set; } // Пробег
        public virtual Model Model { get; set; } 

        public virtual Color Color { get; set; }
        //[ForeignKey("Color")]
        //public  int Color_Id { get; set; }

        public DateTime? MaintenanceDate { get; set; }
        public bool MaintenanceSuccess { get; set; }

        public virtual CarOwner CarOwner { get; set; }
        //public CarDriver CarDriver { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }


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
