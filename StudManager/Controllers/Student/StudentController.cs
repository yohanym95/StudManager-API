using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using StudManager.Data.Models;
using StudManager.Data.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace StudManager.Controllers.Students
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        public StudentController( IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        /// <summary>
        ///     create account for student
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for create account to student")]
        [HttpPost]
        [Route("register")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = _studentServices.ExistUser(model.UserName);

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
                        CourseId = model.CourseId
                    }

                };
                var result = await _studentServices.CreateStudent(user, model.Password);
                
                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student creation failed! Please check user details and try again." });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
        }




        /// <summary>
        ///     update account for student
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for update student account")]
        [HttpPut]
        [Route("update")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Update([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = _studentServices.ExistUser(model.UserName);

                if (userExists == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student is not registered!" });

                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    UserType = "Student",
                    Student = new Student
                    {
                        StudRegNo = model.StudentRegisterNo,
                        FullName = model.FirstName + " " + model.LastName,
                    }
                };

                var result = await _studentServices.UpdateStudent(user);           

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student update process failed! Please check user details and try again." });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return Ok(new ResponseModel { Status = "Success", Message = "User updated successfully!" });
        }

        /// <summary>
        ///     update student password
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for update student account")]
        [HttpPut]
        [Route("update/password")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordModel model)
        {
            try
            {
                var userExists = _studentServices.ExistUser(model.username);

                if (userExists == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student is not registered!" });

                ApplicationUser user = await userExists;

                var result = await _studentServices.ChangePassword(user, model.CurrentPasssword, model.NewPassword);

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Update password process is failed! Please check user details and try again." });
    
                
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return Ok(new ResponseModel { Status = "Success", Message = "Password updated successfully!" });
        }


    }
}
