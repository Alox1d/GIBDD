using DAL;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BLL.Services
{
   public class OffenseService
    {
        public void createOffense(Offense SelectedOffense, TakeStroke SelectedTakeStroke)
        {
            if (SelectedOffense != null)
                if (SelectedOffense.CarDriver.Vehicle.CarOwner != null && SelectedOffense.CarDriver.IsCarOwner)
                {
                    SelectedOffense.CarDriver.FIO = SelectedOffense.CarDriver.Vehicle.CarOwner.FIO;
                    if (SelectedOffense.CarDriver.Vehicle.CarOwner.DriverLicense != null)
                    {
                        double sum = 0;
                        foreach (var VO in SelectedOffense.CarDriver.VehicleOffenses)
                        {
                            sum += VO.Penalty;
                        }
                        SelectedOffense.SumPenalty = sum;
                        //TakeStroke takeStroke = new TakeStroke { TakeDate = DateTime.Now, ReturnDate = DateTime.Now.AddMonths(sum) };
                        if (SelectedTakeStroke != null)
                        {
                            UpdateLicense(SelectedOffense, SelectedTakeStroke);
                        }

                    }
                }

        }

        private static void UpdateLicense(Offense SelectedOffense, TakeStroke SelectedTakeStroke)
        {
            if ((SelectedTakeStroke.ReturnDate - DateTime.Now).TotalSeconds > 0)
            {
                SelectedOffense.CarDriver.Vehicle.CarOwner.DriverLicense.IsInvalid = true;
            }
            SelectedTakeStroke.TakeDate = SelectedOffense.Date;
            SelectedOffense.CarDriver.Vehicle.CarOwner.DriverLicense.TakeStrokes.Add(SelectedTakeStroke);
        }
    }
}
