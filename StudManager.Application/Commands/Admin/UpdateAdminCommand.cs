using MediatR;
using StudManager.Application.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Commands.Admin
{
    public class UpdateAdminCommand : IRequest <Response>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public string StudentRegisterNo { get; set; }
        public int CourseId { get; set; }
    }
}
