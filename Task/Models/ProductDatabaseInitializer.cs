using System.Data.Entity;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace Task.Models
{
    public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            GetReleaseDates().ForEach(r => context.ReleaseDates.Add(r));
            GetDriver().ForEach(d => context.Drivers.Add(d));
            GetProducts().ForEach(p => context.Products.Add(p));
        }
        private static List<ProductReleaseDates> GetReleaseDates()
        {
            var release = new List<ProductReleaseDates>();

            return release;
        }
        private static List<Driver> GetDriver()
        {
            var drivers = new List<Driver>();

            using (FileStream fs = new FileStream("Source\\drivers.json", FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.ASCII))
            using (JsonReader reader = new JsonTextReader(new StringReader(streamReader.ReadToEnd())))
            {
                while(reader.Read())
                {

                }
            }

            return drivers;
        }
        private static List<Product> GetProducts()
        {
            var products = new List<Product>();

            return products;
        }
    }
}