using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Data.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamDescription { get; set; }
        public string ExamName { get; set; }
        public string ExamResult { get; set; }
        public IList<StudentExam> StudentExams { get; set; }

    }
}
