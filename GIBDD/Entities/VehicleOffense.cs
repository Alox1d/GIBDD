using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class VehicleOffense
    {
        public int Id { get; set; }
        public double Penalty { get; set; }
        //public bool? TakeLicense { get; set; }
        public int TakeLicenseTime { get; set; } // в месяцах
        //public virtual CarOwner CarOwner { get; set; }
        public CarDriver CarDriver { get; set; }

        public virtual ArticleOffense ArticleOffense { get; set; }



    }
}
