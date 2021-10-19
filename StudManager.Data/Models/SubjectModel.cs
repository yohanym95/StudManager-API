using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectDescription { get; set; }
        public int Credit { get; set; }
        public int CourseId { get; set; }
    }
}
