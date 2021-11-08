using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Responses.Subjects
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectDescription { get; set; }
        public int Credit { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
