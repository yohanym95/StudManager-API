using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Data.Entities
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
