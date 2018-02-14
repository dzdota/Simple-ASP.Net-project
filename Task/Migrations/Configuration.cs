namespace Task.Migrations
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Task.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Task.Models.ProductContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Task.Models.ProductContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Database.ExecuteSqlCommand("delete from Drivers");
            context.Database.ExecuteSqlCommand("delete from ProductReleaseDates");
            context.Database.ExecuteSqlCommand("delete from Products");
            GetReleaseDates().ForEach(r => context.ReleaseDates.Add(r));
            GetDriver().ForEach(d => context.Drivers.Add(d));
            GetProducts().ForEach(p => context.Products.Add(p));
        }
        private static List<ProductReleaseDates> GetReleaseDates()
        {
            var release = new List<ProductReleaseDates>();

            using (FileStream fs = new FileStream("Source\\products - release dates.csv", FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.ASCII))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Replace(" ", string.Empty);
                    string[] element = line.Split('\n');

                }

            }
            return release;
        }
        private static List<Driver> GetDriver()
        {
            var drivers = new List<Driver>();

            using (FileStream fs = new FileStream("Source\\drivers.json", FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.ASCII))
            using (JsonReader reader = new JsonTextReader(new StringReader(streamReader.ReadToEnd())))
            {
                while (reader.Read())
                {
                    Console.Write(reader.ValueType);
                }
            }

            return drivers;
        }
        private static List<Product> GetProducts()
        {
            var products = new List<Product>();

            products.Add(new Product()
            {
                ProductId = "id21",
                ProductName = "name21"
            });
            products.Add(new Product()
            {
                ProductId = "id22",
                ProductName = "name22"
            });

            products.Add(new Product()
            {
                ProductId = "id23",
                ProductName = "name23"
            });

            products.Add(new Product()
            {
                ProductId = "id24",
                ProductName = "name24"
            });


            return products;
        }
    }
}
