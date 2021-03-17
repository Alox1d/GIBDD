using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CarOwner
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public long PassportData { get; set; }
        public DriverLicense DriverLicense { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; }

        public virtual List<VehicleOffense>? VehicleOffenses { get; set; }

    }
}
