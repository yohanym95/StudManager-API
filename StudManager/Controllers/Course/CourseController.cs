using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudManager.Application.Commands.Courses;
using StudManager.Application.Queries.Courses;
using StudManager.Application.Responses.Courses;
using StudManager.Core.Entities;
using StudManager.Core.Roles;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudManager.Controllers.Courses
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly IMediator _mediator;

        public CourseController(ILogger<CourseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [SwaggerOperation(Summary = "This endpoint use for get all courses")]
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<Course>> Get()
        {
            return await _mediator.Send(new GetAllCourseQuery());
        }

        [SwaggerOperation(Summary = "This endpoint use for get course details")]
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult<CourseResponse>> GetCourse(GetCourseQuery model)
        {
            try
            {
                var course = await _mediator.Send(model);
                return Ok(course);

    }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for create course")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CourseResponse>> Post([FromBody] CourseCommand model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                 return    await _mediator.Send(model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for update course")]
        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CourseResponse>> Update([FromBody] CourseCommand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _mediator.Send(model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for delete the specific course")]
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<DeleteCourseResponse>> DeleteItem(DeleteCourseCommand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _mediator.Send(model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

