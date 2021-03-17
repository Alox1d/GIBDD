using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    // Unit of Work
    public class DBReposSQL : IDbRepos
    {
        private GIBDDContext db;
        private VehicleRepositorySQL vehicleRepositorySQL;
        private CarOwnerRepositorySQL carOwnerRepositorySQL;

        public DBReposSQL()
        {
            //db = new GIBDDContext();
        }
        public IRepository<Vehicle> Vehicle
        {
            get
            {
                if (vehicleRepositorySQL == null)
                    vehicleRepositorySQL = new VehicleRepositorySQL(db);
                return vehicleRepositorySQL;
            }
        }
        public IRepository<CarOwner> CarOwner
        {
            get
            {
                if (carOwnerRepositorySQL == null)
                    carOwnerRepositorySQL = new CarOwnerRepositorySQL(db);
                return carOwnerRepositorySQL;
            }
        }


        public int Save()
        {
            return db.SaveChanges();

        }
    }
}
