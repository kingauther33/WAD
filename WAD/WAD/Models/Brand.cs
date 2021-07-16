using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAD.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name of brand")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } 
    }
}