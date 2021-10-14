using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudManager.Data.Configuration;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using StudManager.Data.Models;
using StudManager.Data.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwaggerOperationAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace StudManager.Controllers.Students
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                var userExists = _unitOfWork.Student.ExistUserByName(model.UserName);

                if (userExists.Result != null)
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
                var result = await _unitOfWork.Student.CreateStudent(user, model.Password);
                
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
        public async Task<IActionResult> Update([FromBody] RegisterModel model, string id)
        {
            try
            {
                var userExists = await _unitOfWork.Student.ExistUserById(id);

                if (userExists == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student is not registered!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Id = id,
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

                var result = _unitOfWork.Student.UpdateStudent(user);           

                if (!(result > 0))
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
                var userExists = _unitOfWork.Student.ExistUserByName(model.username);

                if (userExists.Result == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student is not registered!" });

                ApplicationUser user = await userExists;

                var result = await _unitOfWork.Student.ChangePassword(user, model.CurrentPasssword, model.NewPassword);

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Update password process is failed! Please check user details and try again." });
    
                
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return Ok(new ResponseModel { Status = "Success", Message = "Password updated successfully!" });
        }

        /// <summary>
        ///     Get all students
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for get all students")]
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Get()
        {
            List<ApplicationUser> result;

            try
            {       
                result =  _unitOfWork.Student.GetAllStudents();

                if (result.Count == 0)
                    return Ok(new ResponseModel { Status = "Success", Message = "There is no students for now!" });


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return Ok(result);
        }

        /// <summary>
        ///     Get all students
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for get student details")]
        [HttpGet]
        [Route("")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetStudent(string id)
        {
            ApplicationUser user;

            try
            {
                var userExists = _unitOfWork.Student.GetStudent(id);

                if (userExists == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Student is not registered!" });

                 user = await userExists;

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return Ok(user);
            }


        }
}
