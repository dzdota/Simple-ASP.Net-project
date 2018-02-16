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

    internal sealed class Configuration : DbMigrationsConfiguration<ProductContext>
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

            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Source\\products - release dates.csv", FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.ASCII))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] element = line.Split(';');
                    if (element.Length == 2)
                    {
                        string[] dataelement = element[1].Split(' ','.','-',':');
                        release.Add(new ProductReleaseDates()
                        {
                            ProductId = element[0],
                            RealeseOn = new DateTime(int.Parse(dataelement[0]), int.Parse(dataelement[1]), int.Parse(dataelement[2]),
                            int.Parse(dataelement[3]), int.Parse(dataelement[4]), int.Parse(dataelement[5]), int.Parse(dataelement[6]))
                        });
                    }
                }

            }
            return release;
        }
        private static List<Driver> GetDriver()
        {
            var drivers = new List<Driver>();

            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Source\\drivers.json", FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.ASCII))
            using (JsonReader reader = new JsonTextReader(new StringReader(streamReader.ReadToEnd())))
            {
                while (reader.Read() && (reader.Value == null || reader.Value as string != "Properties"))
                    ;
                var driver = new Driver();
                while (reader.Read())
                {
                    if (reader.Value != null && reader.Value as string == "Properties")
                    {
                        drivers.Add(driver);
                        driver = new Driver();
                    }
                    else if(reader.ValueType == typeof(string))
                        switch (reader.Value as string)
                        {
                            case "CompanyName":
                                reader.Read();
                                driver.CompanyName = reader.Value == null ? "Gamanet A.S." : reader.Value as string;
                                break;
                            case "ProductName":
                                reader.Read();
                                driver.ProductName = reader.Value as string;
                                break;
                            case "Version":
                                reader.Read();
                                driver.Version = reader.Value as string;
                                break;
                            case "Size":
                                reader.Read();
                                driver.Size = (long)reader.Value;
                                break;
                            case "ProductCategory":
                                reader.Read();
                                driver.ProductCategory = reader.Value as string;
                                break;
                        }
                }
            }
            return drivers;
        }
        private static List<Product> GetProducts()
        {
            var products = new List<Product>();


            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Source\\products.csv", FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.ASCII))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] element = line.Split(';');
                    if (element.Length == 4)
                        products.Add(new Product()
                        {
                            ProductId = element[0],
                            ProductName = element[1],
                            Url = element[2] == "NULL" ? null: element[2],
                            VendorContact = element[3] == "NULL" ? null : element[3]
                        });
                }

            }

            return products;
        }
    }
}
