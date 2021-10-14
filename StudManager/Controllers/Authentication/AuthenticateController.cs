using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudManager.Data.Data.Entities;
using StudManager.Data.Models;
using StudManager.Data.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Controllers.Authentication
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        /// <summary>
        ///     login for user
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for login process by creating the token")]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _authenticateService.userAuthenticate(model);

                var results = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(result),
                    expiration = result.ValidTo
                };

                return Created("", results);
            }

            return BadRequest();

        }

    }
}
