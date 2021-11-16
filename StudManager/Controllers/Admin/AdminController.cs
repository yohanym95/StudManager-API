using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudManager.Application.Commands.Admin;
using StudManager.Core.Roles;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace StudManager.Controllers.Admin
{
   // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///     create account for management level user
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for create account to admin")]
        [HttpPost]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterCommand model)
        {
            var result = await _mediator.Send(model);

            if (result.Status == "Error")
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        ///     Update details of management level user
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for update the admin account")]
        [HttpPut]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> Update([FromBody] UpdateAdminCommand model)
        {
            var result = await _mediator.Send(model);

            if (result.Status == "Error")
                return BadRequest(result);

            return Ok(result);
        }

    }
}
