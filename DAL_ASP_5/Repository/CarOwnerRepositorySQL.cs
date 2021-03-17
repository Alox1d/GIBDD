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
    public class CarOwnerRepositorySQL:IRepository<CarOwner>
    {
        private GIBDDContext db;

        public CarOwnerRepositorySQL(GIBDDContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<CarOwner> GetList()
        {
            return db.CarOwners.ToList();
        }

        public CarOwner GetItem(int id)
        {
            return db.CarOwners.Find(id);
        }

        public void Create(CarOwner v)
        {
            db.CarOwners.Add(v);
        }

        public void Update(CarOwner CarOwner)
        {
            db.Entry(CarOwner).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            CarOwner CarOwner = db.CarOwners.Find(id);
            if (CarOwner != null)
                db.CarOwners.Remove(CarOwner);
        }

    }
}
