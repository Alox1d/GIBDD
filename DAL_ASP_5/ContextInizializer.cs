using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL_ASP_5
{
    public class ContextInizializer
    {
        public static void SeedData(GIBDDContext context)
        {
            Color c1 = new Color { Name = "Синий" };
            Color c2 = new Color { Name = "Чёрный" };
            Color c3 = new Color { Name = "Розовый" };
            Model m1 = new Model { Name = "Лада 2107" };
            context.Colors.Add(c1);
            context.Colors.Add(c2);
            context.Colors.Add(c3);
            CarOwner c = new CarOwner { FIO = "Баранов Александр Юрьевич", PassportData = 2419234896 };
            Vehicle v = new Vehicle { RegistrationNumber = "УР122РФ", Color = c1, Model = m1, MaintenanceDate = DateTime.Now, CarOwner = c };
            context.Vehicles.Add(v);
            context.CarOwners.Add(c);

            //var vehicle = context.Vehicles.Find(1);

            ArticleOffense article1 = new ArticleOffense
            {
                Description = "Управление транспортным средством с нечитаемыми, нестандартными или установленными с нарушением требований государственного стандарта государственными регистрационными знаками, за исключением случаев, предусмотренных частью 2 настоящей статьи",
                Number = "12.2.1",
                Penalty = "500 рублей.",
            };
            ArticleOffense article2 = new ArticleOffense
            {
                Description = "Управление транспортным средством без государственных регистрационных знаков, а равно управление транспортным средством без установленных на предусмотренных для этого местах государственных регистрационных знаков либо управление транспортным средством с государственными регистрационными знаками, видоизмененными или оборудованными с применением устройств или материалов, препятствующих идентификации государственных регистрационных знаков либо позволяющих их видоизменить или скрыть",
                Number = "12.2.2",
                Penalty = "5000 рублей или лишение прав от 1 до 3 месяцев"
            };
            context.ArticleOffenses.Add(article1);
            context.ArticleOffenses.Add(article2);
            //var article = context.ArticleOffenses.Find(2);

            VehicleOffense vehicleOffense1 = new VehicleOffense { ArticleOffense = article2, TakeLicenseTime = 3 };
            VehicleOffense vehicleOffense2 = new VehicleOffense { ArticleOffense = article1, TakeLicenseTime = 0, Penalty = 500 };
            var list1 = new List<VehicleOffense>();
            list1.Add(vehicleOffense1);
            list1.Add(vehicleOffense2);
            CarDriver carDriver1 = new CarDriver { FIO = v.CarOwner.FIO, IsCarOwner = true, Vehicle = v, VehicleOffenses = list1 };
            Offense offense1 = new Offense { Address = "г. Иваново, Рабфаковская, 34", Date = DateTime.Now, CarDriver = carDriver1 };
            context.Offenses.Add(offense1);

            Category category1 = new Category { Name = "A" };
            Category category2 = new Category { Name = "B", Vehicles = context.Vehicles.ToList() };
            var cats = new List<Category>();
            cats.Add(category1);
            cats.Add(category2);
            int sum = 0;
            foreach (var item in list1)
            {
                sum += item.TakeLicenseTime;
            }
            TakeStroke takeStroke = new TakeStroke { TakeDate = DateTime.Now, ReturnDate = DateTime.Now.AddMonths(sum) };
            var strokes = new List<TakeStroke>();
            strokes.Add(takeStroke);
            //DriverLicense driverL = new DriverLicense { Number = 7701397000, IsInvalid = false, ReleaseDate = DateTime.Now, Categories = cats, TakeStrokes = strokes };
            //context.DriverLicenses.Add(driverL);
            //int sumTakeTime = 0;
            //foreach (VehicleOffense v12 in offense.CarDriver.VehicleOffenses)
            //{
            //    if (v12.TakeLicenseTime != null)
            //        sumTakeTime += v12.TakeLicenseTime.GetValueOrDefault();
            //}
            //if (offense.CarDriver.IsCarOwner)

            //    if (DateTime.Now.TotalMonths(offense.Date) > 0)
            //{
            //        offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsValid = false;
            //} else
            //{
            //        offense.CarDriver.Vehicle.CarOwner.DriverLicense.IsValid = true;

            //    }
            context.SaveChanges();
        }
    }
}
