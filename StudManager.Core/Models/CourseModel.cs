using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudManager.Core.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Name Required")]
        public string CourseName { get; set; }
        public string Qualifications { get; set; }
        [Required(ErrorMessage = "Course Number Required")]
        public string CourseNo { get; set; }
    }
}
