using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PracticalTest.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Start Time (hh:mm)")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Doesn't match format Type: hh:mm")]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Please Enter Date (dd/MM/yyyy)")]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "Doesn't match format Type: dd/MM/yyyy")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please Enter Date (in minutes)")]
        [Range(0,300)]
        public int Duration { get; set; }

        public int SubjectID { get; set; } // khoa ngoai
        public int FacultyID { get; set; }
        public int ClassRoomID { get; set; }
        public int StatusID { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }
        public virtual Status Status { get; set; }
    }
}