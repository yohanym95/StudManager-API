using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudManager.Data.Configuration;
using StudManager.Data.Data.Roles;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace StudManager.Controllers.Fees
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeesController : Controller
    {
        private readonly ILogger<FeesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeesController(ILogger<FeesController> logger, IUnitOfWork unitOfWork, IMapper mapper)
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
                var users = await _unitOfWork.Fees.All();
                return Ok(users);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
