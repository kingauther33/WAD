using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssignmentWAD.Models;
using System.Data.Entity;

namespace AssignmentWAD.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Asm_WAD") { }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }

        public System.Data.Entity.DbSet<AssignmentWAD.Models.User> Users { get; set; }
    }
}