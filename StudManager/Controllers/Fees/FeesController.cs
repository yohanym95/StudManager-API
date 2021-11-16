using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudManager.Application.Commands.Fee;
using StudManager.Application.Queries.Fee;
using StudManager.Core.Roles;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace StudManager.Controllers.Fee
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeesController : Controller
    {

        private readonly IMediator _mediator;
        public FeesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [SwaggerOperation(Summary = "This endpoint use for get Fees details")]
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> GetFees(GetFeesQuery model)
        {
            try
            {
                var fees = await _mediator.Send(model);
                return Ok(fees);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for Add Fees")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] CreateFessCommand model)
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
    }
}
