using StudManager.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Core.Repositories
{
    public interface IAuthenticateService
    {
        Task<JwtSecurityToken> userAuthenticate(LoginModel model);
    }
}
