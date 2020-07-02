using DbBackuperBL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace DbBackuperCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingContext db;

            Console.WriteLine("Какую операцию с БД выполнить?\n1 - создать резервную копию всех таблиц БД.\n2 - восстановить БД из резервной копии.\n3 - завершить работу программы.");

            int chose = 0;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out chose))
                {
                    switch (chose)
                    {
                        case 1:
                            db = new ParkingContext();
                            List<Client> clients = db.Clients.ToList();
                            List<Car> cars = db.Cars.ToList();

                            BinaryFormatter serializer = new BinaryFormatter();
                            using (FileStream fs = new FileStream("Clients.bin", FileMode.OpenOrCreate))
                            {
                                serializer.Serialize(fs, clients);
                                Console.WriteLine("Сериализация db.Clients завершена.");
                            }
                            using (FileStream fs = new FileStream("Cars.bin", FileMode.OpenOrCreate))
                            {
                                serializer.Serialize(fs, cars);
                                Console.WriteLine("Сериализация db.Cars завершена.");
                            }
                            break;
                        case 2:
                            BinaryFormatter deserializer = new BinaryFormatter();
                            List<Client> tempClients;
                            List<Car> tempCars;
                            using (FileStream fs = new FileStream("Clients.bin", FileMode.OpenOrCreate))
                            {
                                tempClients = deserializer.Deserialize(fs) as List<Client>;
                            }
                            using (FileStream fs = new FileStream("Cars.bin", FileMode.OpenOrCreate))
                            {
                                tempCars = deserializer.Deserialize(fs) as List<Car>;
                            }
                            using (db = new ParkingContext())
                            {
                                db.Clients.AddRange(tempClients);
                                db.Cars.AddRange(tempCars);
                                db.SaveChanges();
                            }
                            Console.WriteLine("База была восстановлена из резервной копии.");
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Команда не распознана.\n1 - создать резервную копию всех таблиц БД.\n2 - восстановить БД из резервной копии.\n3 - завершить работу программы.");
                }
            }

            Console.ReadLine();
        }
    }
}
