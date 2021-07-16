using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PracticalTest.Models;

namespace PracticalTest.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("PracticalWAD") { }

        public DbSet<ClassRoom> ClassRooms { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public System.Data.Entity.DbSet<PracticalTest.Models.Status> Status { get; set; }
    }
}