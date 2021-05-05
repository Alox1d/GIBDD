using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public partial class GIBDDContext : IdentityDbContext<User>
    {

        public GIBDDContext(DbContextOptions options) : base(options)
        { }

        public DbSet<ArticleOffense> ArticleOffenses { get; set; }
        public DbSet<CarDriver> CarDrivers { get; set; }
        public DbSet<CarOwner> CarOwners { get; set; }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Offense> Offenses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<VehicleOffense> VehicleOffenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarOwner>()
                .HasOne(c => c.DriverLicense)
                .WithOne(c => c.CarOwner);

            //        modelBuilder.Entity<CarDriver>()
            //.HasOptional(c => c.Vehicle)
            //.WithOptionalPrincipal(c => c.CarDrivers);



        }
    }
}
