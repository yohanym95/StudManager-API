using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudManager.Data.Configuration;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using StudManager.Data.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudManager.Controllers.Subjects
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubjectController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectController(ILogger<SubjectController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "This endpoint use for get all subject with Details")]
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var subjects = await _unitOfWork.Subject.All();
                return Ok(subjects);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for get specific subject details")]
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var subject = await _unitOfWork.Subject.GetById(id);

                if (subject == null)
                    return NotFound();

                return Ok(subject);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for Add Subject")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] SubjectModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var subject = _mapper.Map<SubjectModel, Subject>(model);

                    await _unitOfWork.Subject.Add(subject);
                    await _unitOfWork.CompleteAsync();

                    return Ok(subject);
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

        [SwaggerOperation(Summary = "This endpoint use for update subject")]
        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Update([FromBody] SubjectModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var subject = _mapper.Map<SubjectModel, Subject>(model);

                    await _unitOfWork.Subject.Upsert(subject);
                    await _unitOfWork.CompleteAsync();

                    return Ok(subject);
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
