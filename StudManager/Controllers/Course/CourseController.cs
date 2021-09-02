using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudManager.Data.Configuration;
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
        private readonly ILogger<CourseController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseController(ILogger<CourseController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [SwaggerOperation(Summary = "This endpoint use for get all courses")]
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _unitOfWork.Courses.All();
                return Ok(users);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for get course details")]
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                var item = await _unitOfWork.Courses.GetById(id);

                if (item == null)
                    return NotFound();

                return Ok(item);

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

                    var course = _mapper.Map<CourseModel, Course>(model);

                    await _unitOfWork.Courses.Add(course);
                    await _unitOfWork.CompleteAsync();

                    //return CreatedAtAction("GetItem", new { course.Id }, course);
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
        }

        [SwaggerOperation(Summary = "This endpoint use for update course")]
        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Update([FromBody] CourseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var course = _mapper.Map<CourseModel, Course>(model);

                    await _unitOfWork.Courses.Upsert(course);
                    await _unitOfWork.CompleteAsync();
                    
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
        }

        [SwaggerOperation(Summary = "This endpoint use for delete the specific course")]
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _unitOfWork.Courses.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Courses.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}

