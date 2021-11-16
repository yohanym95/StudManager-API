using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudManager.Application.Commands.Authenticate;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace StudManager.Controllers.Authentication
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     login for user
        /// </summary>
        /// <response code="401">Unauthorized access</response>
        [SwaggerOperation(Summary = "This endpoint use for login process by creating the token")]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand model)
        {

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(model);

                return Created("", result);
            }

            return BadRequest();

        }

    }
}
