using DAL;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BLL.Services
{
    public class DriverLicenseService
    {
        //private GIBDDContext db;
        public DriverLicenseService(GIBDDContext repos)
        {
            //db = repos;
        }

        public int CheckLicense(List<DriverLicense> driverLicenses)
        {
            int count = 0;
            foreach (var license in driverLicenses)
            {
                if(license.TakeStrokes.Count == 0 && license.IsInvalid == true)
                {
                    license.IsInvalid = false;
                    count++;
                }
                foreach (var takeStroke in license.TakeStrokes)
                {
                    if ((takeStroke.ReturnDate - DateTime.Now).TotalSeconds > 0)
                    {
                        if (license.IsInvalid == false)
                        {
                            count++;
                        }
                        license.IsInvalid = true;
                        continue;
                    }
                    else
                    {
                        if (license.IsInvalid == true)
                        {
                            count++;
                        }
                        license.IsInvalid = false;
                    }

                    //int sumTakeTime = 0;
                    //    foreach (VehicleOffense VO in offense.CarDriver.VehicleOffenses)
                    //    {
                    //        if (VO.TakeLicenseTime != 0)
                    //            sumTakeTime += VO.TakeLicenseTime;
                    //    }
                    //    if (offense.CarDriver.IsCarOwner)

                    //        if (DateTime.Now.TotalMonths(offense.Date) > sumTakeTime)
                    //        {
                    //            if (offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsInvalid == true)
                    //            {
                    //                count++;
                    //            }
                    //            offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsInvalid = false;
                    //        }
                    //        else
                    //        {
                    //            if (offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsInvalid == false)
                    //            {
                    //                count++;
                    //            }
                    //            offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsInvalid = true;

                    //        }
                }

            }
            return count;
        }


        //db.SaveChanges();

    }
}
public static class DateTimeExtensions
{
    public static int TotalMonths(this DateTime start, DateTime end)
    {
        return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
    }
}

//foreach(var license in driverLicenses)
//{
//    foreach (VehicleOffense VO in license.CarOwner.)
//    {
//        if (v12.TakeLicenseTime != null)
//            sumTakeTime += v12.TakeLicenseTime.GetValueOrDefault();
//    }
//    if (offense.CarDriver.IsCarOwner)

//        if (DateTime.Now.TotalMonths(offense.Date) > 0)
//        {
//            offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsValid = false;
//        }
//        else
//        {
//            offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsValid = true;

//        }
//    if (DateTime.Now.TotalMonths(offense.Date) > 0)
//    {
//        offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsValid = false;
//    }
//    else
//    {
//        license.IsInvalid = true;

//    }
//}
//int sumTakeTime = 0;