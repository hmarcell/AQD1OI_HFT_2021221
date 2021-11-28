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
            //var result = rest.Get<KeyValuePair<string, double?>>("stat/avgpricebybrands");
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}
            //AVGPriceByBrands
            //var nonCrudMenu = new ConsoleMenu(args, level: 1)
            //    .Add("Average prices by brands", () => {
            //        var result = rest.Get<KeyValuePair<string, double?>>("stat/avgpricebybrands");
            //        foreach (var item in result)
            //        {
            //            Console.WriteLine(item);
            //        }
            //    })
            //    .Add("Sub_Two", () => Console.WriteLine("2"))
            //    .Add("Sub_Three", () => Console.WriteLine("3"))
            //    .Add("Sub_Four", () => Console.WriteLine("4"))
            //    .Add("Close", ConsoleMenu.Close)
            //    .Configure(config =>
            //    {
            //        config.Selector = " >";
            //        //config.EnableFilter = true;
            //        config.Title = "Non-crud";
            //        config.EnableBreadcrumb = true;
            //        config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            //    });

            //var menu = new ConsoleMenu(args, level: 0)
            //  .Add("Bikes", () => Console.WriteLine("1"))
            //  .Add("Brands", () => Console.WriteLine("2"))
            //  .Add("Rentals", () => Console.WriteLine("3"))
            //  .Add("Non-Crud", nonCrudMenu.Show)
            //  .Add("Change me", (thisMenu) => thisMenu.CurrentItem.Name = "I am changed!")
            //  .Add("Close", ConsoleMenu.Close)
            //  .Add("Action then Close", (thisMenu) => { Console.WriteLine("close") ; thisMenu.CloseMenu(); })
            //  .Add("Exit", () => Environment.Exit(0))
            //  .Configure(config =>
            //  {
            //      config.Selector = " >";
            //      //config.EnableFilter = true;
            //      config.Title = "Main menu";
            //      config.EnableWriteTitle = true;
            //      config.EnableBreadcrumb = true;
            //  });

            //menu.Show();
        }
    }
}
