using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudManager.Application.Commands.Subjects;
using StudManager.Application.Queries.Subjects;
using StudManager.Core.Roles;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace StudManager.Controllers.Subjects
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubjectController : Controller
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(Summary = "This endpoint use for get all subject with Details")]
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var subjects = await _mediator.Send(new GetAllSubjectQuery());
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
        public async Task<IActionResult> Get(GetSubjectQuery model)
        {
            try
            {
                var subject = await _mediator.Send(model);

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
        public async Task<IActionResult> Post([FromBody] CreateSubjectCommand model)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var result = await _mediator.Send(model);

                    return Ok(result);
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
        public async Task<IActionResult> Update([FromBody] UpdateSubjectCommand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _mediator.Send(model);

                    return Ok(result);
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
