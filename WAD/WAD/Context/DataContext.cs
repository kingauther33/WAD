using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WAD.Models;

namespace WAD.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("WAD")
        {

        }
        public DbSet<Category> Categories { get; set; }

        public System.Data.Entity.DbSet<WAD.Models.Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}