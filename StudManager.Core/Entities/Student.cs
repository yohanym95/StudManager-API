using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudManager.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string StudRegNo { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Fees Fees { get; set; }
    }
}
