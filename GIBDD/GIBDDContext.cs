using GIBDD.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD
{
    public class GIBDDContext : DbContext
    {
        public GIBDDContext()
    : base()
        { }

        public DbSet<ArticleOffense> ArticleOffenses { get; set; }
        //public DbSet<CarDriver> CarDrivers { get; set; }
        public DbSet<CarOwner> CarOwners { get; set; }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        //public DbSet<TakeStroke> TakeStrokes { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<LicenseCategory> LicenseCategories { get; set; }
        //public DbSet<Offense> Offenses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        //public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<VehicleOffense> VehicleOffenses { get; set; }
    }
}
