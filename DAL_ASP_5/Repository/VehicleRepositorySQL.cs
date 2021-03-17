using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class VehicleRepositorySQL:IRepository<Vehicle>
    {
        private GIBDDContext db;

        public VehicleRepositorySQL(GIBDDContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Vehicle> GetList()
        {
            return db.Vehicles.ToList();
        }

        public Vehicle GetItem(int id)
        {
            return db.Vehicles.Find(id);
        }

        public void Create(Vehicle v)
        {
            db.Vehicles.Add(v);
        }

        public void Update(Vehicle Vehicle)
        {
            db.Entry(Vehicle).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Vehicle Vehicle = db.Vehicles.Find(id);
            if (Vehicle != null)
                db.Vehicles.Remove(Vehicle);
        }


    }
}
