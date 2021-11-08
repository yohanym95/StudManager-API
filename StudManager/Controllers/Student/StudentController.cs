using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudManager.Application.Commands.Students;
using StudManager.Application.Queries.Courses;
using StudManager.Application.Queries.Students;
using StudManager.Core.Roles;
using System;
using System.Threading.Tasks;
using SwaggerOperationAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace StudManager.Controllers.Students
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     create account for student
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for create account to student")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand model)
        {
            try
            {
                var result = await _mediator.Send(model);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        ///     update account for student
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for update student account")]
        [HttpPut]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Update([FromBody] UpdateCommand model)
        {
            try
            {
                var result = await _mediator.Send(model);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        ///     update student password
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for update the password of student account")]
        [HttpPut]
        [Route("/password")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand model)
        {
            try
            {
                var result = await _mediator.Send(model);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        ///     Get all students
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for get all students")]
        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Get()
        {
           

            try
            {
                var result = await _mediator.Send(new GetAllCourseQuery());
                return Ok(result);


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        ///     Get all students
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for get student details")]
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetStudent(GetStudentQuery model)
        {

            try
            {
                var result = await _mediator.Send(model);
                if (result == null)
                    return NotFound();

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            }


        }
}
