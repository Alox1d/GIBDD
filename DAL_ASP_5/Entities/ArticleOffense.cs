using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ArticleOffense
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Penalty { get; set; } // штраф

        public virtual List<VehicleOffense>? VehicleOffenses { get; set; }

    }
}
