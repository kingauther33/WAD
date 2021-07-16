using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AssignmentWAD.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "Please enter name of category")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter image source of the category")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Please enter description of the category")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } // Has Many
    }
}