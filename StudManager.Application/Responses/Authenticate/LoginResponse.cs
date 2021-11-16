using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Responses.Authenticate
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
