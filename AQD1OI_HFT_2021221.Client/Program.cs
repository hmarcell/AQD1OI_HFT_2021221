using AQD1OI_HFT_2021221.Models;
using CarDB.Client;
using ConsoleTools;
using System;
using System.Collections.Generic;

namespace AQD1OI_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:7293");

            
            var nonCrudMenu = new ConsoleMenu(args, level: 1)                                   //non-crud menu
                .Add("Average prices by brands", () =>
                {
                    var result = rest.Get<KeyValuePair<string, double?>>("stat/avgpricebybrands");
                    ToConsole(result);
                })
                .Add("Renters of the most expensive bike", () =>
                {
                    var result = rest.Get<string>("stat/mostexpensivebikerenters");
                    ToConsole(result);

                })
                .Add("Renters and rental dates of a specific bike", () =>
                {
                    Console.WriteLine("Please enter a model name (e.g. Csepel Budapest A)");
                    string input = Console.ReadLine();
                    var result = rest.Get<KeyValuePair<string, DateTime>>($"stat/datesandrenters/{input}");
                    ToConsole(result);
                })
                .Add("Rentals per bike", () => 
                {
                    var result = rest.Get<KeyValuePair<string, int>>("stat/rentalsperbike");
                    ToConsole(result);
                })
                .Add("Bike models and rental dates", () =>
                {
                    var result = rest.Get<KeyValuePair<string, DateTime>>("stat/ModelsAndDates");
                    ToConsole(result);
                })
                .Add("Earnings by bikes", () =>
                {
                    var result = rest.Get<KeyValuePair<string, int?>>("stat/earningsbybikes");
                    ToConsole(result);
                })
                .Add("Close", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = " >";
                    //config.EnableFilter = true;
                    config.Title = "Non-crud";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });


            var bikeMenu = new ConsoleMenu(args, level: 1)                                   //bike menu
                .Add("Get all bikes", () =>
                {
                    var result = rest.Get<Bike>("bike");
                    ToConsole(result);
                })
                .Add("Get a bike by id", () =>
                {
                    Console.WriteLine("Please enter a bike id");
                    var result = rest.GetSingle<Bike>($"bike/{Console.ReadLine()}");
                    Console.WriteLine(result);
                    Console.WriteLine("Press enter to continue!");
                    Console.ReadLine();

                })
                .Add("Create a bike", () =>
                {
                    Console.WriteLine("Model:");
                    string model = Console.ReadLine();
                    Console.WriteLine("Price:");
                    string input = Console.ReadLine();
                    int? price;
                    if (input != "")
                    {
                        price = int.Parse(input);
                    }
                    else
                    {
                        price = 0;
                    }
                    Console.WriteLine("Brand ID: (e.g. 1)");
                    input = Console.ReadLine();
                    int brandid;
                    if (input != "")
                    {
                        brandid = int.Parse(input);
                    }
                    else
                    {
                        brandid = 1;
                    }
                    rest.Post(new Bike() { Model = model, Price = price, BrandID = brandid}, "bike");
                })
                .Add("Update a bike", () =>
                {
                    Console.WriteLine("ID:");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Model:");
                    string model = Console.ReadLine();
                    Console.WriteLine("Price:");
                    string input = Console.ReadLine();
                    int? price;
                    if (input != "")
                    {
                        price = int.Parse(input);
                    }
                    else
                    {
                        price = 0;
                    }
                    Console.WriteLine("Brand ID: (e.g. 1)");
                    input = Console.ReadLine();
                    int brandid;
                    if (input != "")
                    {
                        brandid = int.Parse(input);
                    }
                    else
                    {
                        brandid = 1;
                    }
                    rest.Put(new Bike() {ID = id ,Model = model, Price = price, BrandID = brandid }, "bike");
                })
                .Add("Delete a bike", () =>
                {
                    Console.WriteLine("Id:");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "bike");
                })
                .Add("Close", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = " >";
                    //config.EnableFilter = true;
                    config.Title = "Bikes";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            var brandMenu = new ConsoleMenu(args, level: 1)                                   //brand menu
                .Add("Get all brands", () =>
                {
                    var result = rest.Get<Brand>("brand");
                    ToConsole(result);
                })
                .Add("Get a brand by id", () =>
                {
                    Console.WriteLine("Please enter a brand id");
                    var result = rest.GetSingle<Brand>($"brand/{Console.ReadLine()}");
                    Console.WriteLine(result);
                    Console.WriteLine("Press enter to continue!");
                    Console.ReadLine();

                })
                .Add("Create a brand", () =>
                {
                    Console.WriteLine("Name:");
                    string name = Console.ReadLine();
                    rest.Post(new Brand() {Name = name}, "brand");
                })
                .Add("Update a brand", () =>
                {
                    Console.WriteLine("ID:");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Name:");
                    string name = Console.ReadLine();
                    rest.Put(new Brand() {ID = id, Name = name }, "brand");
                })
                .Add("Delete a brand", () =>
                {
                    Console.WriteLine("Id:");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "brand");
                })
                .Add("Close", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = " >";
                    //config.EnableFilter = true;
                    config.Title = "Brands";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });
            var rentalMenu = new ConsoleMenu(args, level: 1)                                   //rental menu
                .Add("Get all rentals", () =>
                {
                    var result = rest.Get<Rental>("rental");
                    ToConsole(result);
                })
                .Add("Get a rental by id", () =>
                {
                    Console.WriteLine("Please enter a rental id");
                    var result = rest.GetSingle<Rental>($"rental/{Console.ReadLine()}");
                    Console.WriteLine(result);
                    Console.WriteLine("Press enter to continue!");
                    Console.ReadLine();

                })
                .Add("Create a rental", () =>
                {
                    Console.WriteLine("Renter:");
                    string renter = Console.ReadLine();
                    Console.WriteLine("BikeId");
                    int bikeId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Date: (Please use the following format: yyyy-mm-dd)");
                    string[] dateSplit = Console.ReadLine().Split('-');
                    int year = int.Parse(dateSplit[0]);
                    int month = int.Parse(dateSplit[1]);
                    int day = int.Parse(dateSplit[2]);

                    rest.Post(new Rental() { Renter = renter, BikeID = bikeId, Date = new DateTime(year,month,day) }, "rental");
                })
                .Add("Update rental", () =>
                {
                    Console.WriteLine("ID:");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Renter:");
                    string renter = Console.ReadLine();
                    Console.WriteLine("BikeId");
                    int bikeId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Date: (Please use the following format: yyyy-mm-dd)");
                    string[] dateSplit = Console.ReadLine().Split('-');
                    int year = int.Parse(dateSplit[0]);
                    int month = int.Parse(dateSplit[1]);
                    int day = int.Parse(dateSplit[2]);

                    rest.Put(new Rental() { ID = id ,Renter = renter, BikeID = bikeId, Date = new DateTime(year, month, day) }, "rental");
                })
                .Add("Delete rental", () =>
                {
                    Console.WriteLine("Id:");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "rental");
                })
                .Add("Close", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = " >";
                    //config.EnableFilter = true;
                    config.Title = "Rentals";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });



            var menu = new ConsoleMenu(args, level: 0)
              .Add("Bikes", bikeMenu.Show)
              .Add("Brands", brandMenu.Show)
              .Add("Rentals", rentalMenu.Show)
              .Add("Non-Crud", nonCrudMenu.Show)
              .Add("Exit", () => Environment.Exit(0))
              .Configure(config =>
              {
                  config.Selector = " >";
                  //config.EnableFilter = true;
                  config.Title = "Main menu";
                  config.EnableWriteTitle = true;
                  config.EnableBreadcrumb = true;
              });

            menu.Show();
        }

        public static void ToConsole<T>(IEnumerable<T> input)
        {
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press enter to continue!");
            Console.ReadLine();
        }
    }
}
