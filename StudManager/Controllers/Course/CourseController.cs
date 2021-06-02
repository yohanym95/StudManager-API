using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudManager.Data.Data.Entities;
using StudManager.Data.Models;
using StudManager.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudManager.Controllers.Courses
{
    [Route("api/[Controller]")]
    [ApiController]
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _course;

        public CourseController(ICourseService course)
        {
            _course = course;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var courses = _course.GetAllCourse();
                
                if(courses.Count() > 0)
                {
                    return Ok(courses);
                }
                return BadRequest("There is no courses");

            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
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

                    _course.AddCourse(newCourse);

                    if (_course.SaveAll())
                    {
                        return Ok(newCourse);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }catch(Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Failed to Saved New Course Module");
        }
    }
}
