using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.Entities
{
    public class ArticleOffense
    {
        public int Id { get; set; }
        public double Number { get; set; }
        public string Description { get; set; }
        public string? Penalty { get; set; } // штраф
        public string? TakeLicenseTime { get; set; } // в месяцах

        public virtual ICollection<VehicleOffense>? VehicleOffenses { get; set; }

    }
}
