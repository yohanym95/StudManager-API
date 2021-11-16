using MediatR;
using StudManager.Application.Responses.Authenticate;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Commands.Authenticate
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
