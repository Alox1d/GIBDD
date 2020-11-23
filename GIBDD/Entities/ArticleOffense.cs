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
        public int Number { get; set; }
        public string Description { get; set; }
        public double? Penalty { get; set; } // штраф
        public int? TakeLicenseTime { get; set; } // в месяцах

        public ICollection<VehicleOffense>? VehicleOffenses { get; set; }

    }
}
