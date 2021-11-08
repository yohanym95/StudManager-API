using MediatR;
using StudManager.Application.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Commands.Students
{
    public class UpdatePasswordCommand : IRequest<Response>
    {
        public string username { get; set; }
        public string CurrentPasssword { get; set; }
        public string NewPassword { get; set; }
    }
}
