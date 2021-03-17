using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CarDriver
    {
        public int Id { get; set; }
        public bool IsCarOwner { get; set; }
        public string? FIO { get; set; }

        public Vehicle? Vehicle { get; set; }
        //public virtual DriverLicense? DriverLicense { get; set; }
        public List<VehicleOffense>? VehicleOffenses { get; set; }
        public List<Offense> Offenses { get; set; }

        public CarDriver()
        {
            //VehicleOffenses = new List<VehicleOffense>();
        }

    }
}
