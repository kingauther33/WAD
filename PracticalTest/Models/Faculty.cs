using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PracticalTest.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FacultyName { get; set; }
    }
}