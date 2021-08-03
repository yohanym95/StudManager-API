using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using StudManager.Data.Models;
using StudManager.Data.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudManager.Controllers.Courses
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _course;
        private readonly IMapper _mapper;

        public CourseController(ICourseService course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "This endpoint use for get all courses")]
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Get()
        {
            try
            {
                var courses = _course.GetAllCourse();

                if (courses.Count() > 0)
                {
                    return Ok(courses);
                }
                return BadRequest("There is no courses");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for get course details")]
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult GetCourse(int id)
        {
            try
            {
                var courses = _course.GetCourse(id);

                if (courses != null)
                {
                    return Ok(courses);
                }
                return BadRequest("There is no course");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for create course")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] CourseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCourse = new Course()
                    {
                        CourseName = model.CourseName,
                        Qualifications = model.Qualifications,
                        CourseNo = model.CourseNo
                    };

                    var course = _mapper.Map<Course>(model);

                    _course.AddCourse(course);

                    if (_course.SaveAll())
                    {
                        return Ok(course);
                    }
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
            return BadRequest("Failed to Saved New Course Module");
        }

        [SwaggerOperation(Summary = "This endpoint use for update course")]
        [HttpPut]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Update([FromBody] CourseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCourse = new Course()
                    {
                        CourseName = model.CourseName,
                        Qualifications = model.Qualifications,
                        CourseNo = model.CourseNo
                    };

                    var course = _mapper.Map<Course>(model);

                    _course.UpdateCourse(course);

                    return Ok(course);
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

            return BadRequest("Failed to Saved New Course Module");
        }
    }
}

