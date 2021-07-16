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
        [Required(ErrorMessage = "Please Enter Start Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:mm", ApplyFormatInEditMode = true)]
        public string Duration { get; set; }

        public int SubjectID { get; set; } // khoa ngoai
        public int FacultyID { get; set; }
        public int ClassRoomID { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }
    }
}