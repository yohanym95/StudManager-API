using StudManager.Data.Data.Entities;
using StudManager.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Services
{
    public interface IAuthenticateService
    {
        Task<JwtSecurityToken> userAuthenticate(LoginModel model);
    }
}
