using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
         
        public string RegistrationNumber { get; set; }
        public virtual Model Model { get; set; } 

        public virtual Color Color { get; set; }
        //[ForeignKey("Color")]
        //public  int Color_Id { get; set; }

        public DateTime? MaintenanceDate { get; set; }
        public bool MaintenanceSuccess { get; set; }

        public virtual CarOwner CarOwner { get; set; }

        public virtual DriverLicense DriverLicense { get; set; }
        public virtual Category Category { get; set; }


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
