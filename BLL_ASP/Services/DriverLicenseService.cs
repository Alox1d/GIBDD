using DAL.Entities;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class DriverLicenseService
    {

        public DriverLicenseService()
        {

        }

        public int CheckLicense(List<DriverLicense> driverLicenses)
        {
            int count = 0;
            foreach (var license in driverLicenses)
            {
                if (license.TakeStrokes.Count == 0 && license.IsInvalid == true)
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

                }

            }
            return count;
        }
    }
}
public static class DateTimeExtensions
{
    public static int TotalMonths(this DateTime start, DateTime end)
    {
        return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
    }
}