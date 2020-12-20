using DAL;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BLL.Services
{
    public class MaintenanceService 
    {
        private GIBDDContext db;
        public MaintenanceService(GIBDDContext repos)
        {
            db = repos;
        }

        public int CheckMaintenance(List<Vehicle> vehicles)
        {

            //List<VehicleModel> orderedphones = db.Vehicle.ToList()
            //vehicles
            int count = 0;
            foreach (var v in vehicles)
            {
                if (v.MaintenanceDate == null) continue;
                var date = v.MaintenanceDate.Value;
                var dbV = db.Vehicles.Find(v.Id);
                if ((DateTime.Now - date).TotalDays > 365 * 2)
                {
                    if (!dbV.MaintenanceSuccess)
                    {
                        count++;

                    }
                    dbV.MaintenanceSuccess = true;
                }
                else
                {
                    dbV.MaintenanceSuccess = false;
                }
                db.Entry(dbV).State = EntityState.Modified;
                //db.Vehicle.Update(dbV);

            }

            //int i = db.SaveChanges();
            db.SaveChanges();
            return count;

        }
    }
}
