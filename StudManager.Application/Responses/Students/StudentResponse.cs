using MediatR;
using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Responses.Students
{
    public class StudentResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string UserType { get; set; }
        public virtual Student Student { get; set; }
        public virtual Admin Admin { get; set; }
    }
}
