using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudManager.Core.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string AdminRegNo { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [ForeignKey("ApplicationUser")]
        public string AdminId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
