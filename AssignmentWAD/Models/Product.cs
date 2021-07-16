using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentWAD.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int BrandID { get; set; } // Foreign Key

        public virtual Brand Category { get; set; } // Belongs to
    }
}