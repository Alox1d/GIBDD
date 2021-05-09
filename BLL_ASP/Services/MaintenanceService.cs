
using DAL.Entities;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class MaintenanceService 
    {
        public MaintenanceService()
        {
        }

        public int CheckMaintenance(List<Vehicle> vehicles)
        {

            int count = 0;
            foreach (var v in vehicles)
            {
                if (v.MaintenanceDate == null) continue;
                var date = v.MaintenanceDate.Value;
                //var dbV = db.Vehicles.Find(v.Id);
                if ((DateTime.Now - date).TotalDays > 365 * 2)
                {
                    if (!v.MaintenanceSuccess)
                    {
                        count++;

                    }
                    v.MaintenanceSuccess = true;
                }
                else
                {
                    v.MaintenanceSuccess = false;
                }
                //db.Entry(dbV).State = EntityState.Modified;

            }
            return count;

        }
    }
}
