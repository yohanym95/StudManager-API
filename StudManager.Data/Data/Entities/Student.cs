using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudManager.Data.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string StudRegNo { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
