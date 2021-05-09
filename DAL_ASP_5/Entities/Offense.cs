using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Offense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public double SumPenalty { get; set; }
        public CarDriver CarDriver { get; set; }
        [NotMapped]
        public DateTime ReturnDate { get; set; }
        [NotMapped]
        public bool IsTakeLicense { get; set; }

    }
}
