using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DriverLicense
    {
        public int Id { get; set; }
        public long Number { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsInvalid { get; set; }
        public virtual List<TakeStroke>? TakeStrokes { get; set; }
        //public ICollection<LicenseCategory> LicenseCategories { get; set; }
        public virtual List<Category> Categories { get; set; }
        public CarOwner CarOwner { get; set; }
        public int CarOwnerId { get; set; }

    }


    public class TakeStroke
    {
        public int Id { get; set; }
        public DateTime TakeDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DriverLicense? DriverLicense { get; set; }

    }
    //public class LicenseCategory
    //{
    //    public int Id { get; set; }
    //    public DateTime AssignDate { get; set; }
    //    public DateTime? TakeDate { get; set; }
    //    public Category Category { get; set; }
    //    public DriverLicense? DriverLicense { get; set; }


    //}
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<LicenseCategory> LicenseCategories { get; set; }
        public virtual List<DriverLicense> DriverLicenses { get; set; }
        public List<Vehicle> Vehicles { get; set; }

    }
}
