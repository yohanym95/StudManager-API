using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Qualifications { get; set; }
        public string CourseNo { get; set; }
        public virtual Fees Fees { get; set; } 
    }
}
