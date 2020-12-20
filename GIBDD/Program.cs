using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GIBDDContext db = new GIBDDContext())
            {
                // создаем два объекта User
                Color user1 = new  Color { Name = "Синий" };
                Color user2 = new Color { Name = "Чёрный" };
                CarOwner user3 = new CarOwner { FIO = "Чёрный", PassportData = 123 };

                //добавляем их в бд
                db.Colors.Add(user1);
                db.Colors.Add(user2);
                db.CarOwners.Add(user3);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.CarOwners;
                Console.WriteLine("Список объектов:");
                foreach (CarOwner c in users)
                {
                    Console.WriteLine("{0}.{1}", c.Id, c.FIO);
                }
            }
            Console.Read();
        }
    }
}
