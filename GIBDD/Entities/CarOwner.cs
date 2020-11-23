using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.Entities
{
    public class CarOwner
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int PassportData { get; set; }
        public ICollection<DriverLicense> DriverLicenses { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<VehicleOffense>? VehicleOffenses { get; set; }

    }
}
