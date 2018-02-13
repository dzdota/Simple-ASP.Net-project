using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Task.Models
{
    public class ProductContext: DbContext
    {
        public ProductContext() : base("Task")
        {
        }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReleaseDates> ReleaseDates{ get; set; }
    }
}