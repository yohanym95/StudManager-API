using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using StudManager.Data.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuraion;
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> _signInManager, IConfiguration configuration)
        {
            _configuraion = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
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
                var user = await _userManager.FindByNameAsync(model.UserName);
                if(user != null && await _userManager.CheckPasswordAsync(user, model.Password)){
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    foreach(var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuraion["Tokens:key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                    issuer: _configuraion["Tokens:Issuer"],
                    audience: _configuraion["Tokens:Audience"],
                    expires: DateTime.Now.AddHours(1) ,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Created("", results);
                }
            }

            return BadRequest();

        }

        /// <summary>
        ///     create account for student
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for create account to student")]
        [HttpPost]
        [Route("register/student")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    UserType = model.UserType,
                    Student = new Student
                    {
                        StudRegNo = model.StudentRegisterNo,
                        FullName = model.FirstName + " " + model.LastName,
                    }

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student creation failed! Please check user details and try again." });

                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
                Debug.WriteLine(result);

            }catch(Exception e)
            {
                return BadRequest(e);
            }
            

            return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
        }

        /// <summary>
        ///     create account for admin
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for create account to admin")]
        [HttpPost]
        [Route("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                UserType = model.UserType
                
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
        }
    }
}
