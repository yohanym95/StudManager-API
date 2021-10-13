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
using System.Threading.Tasks;

namespace StudManager.Controllers.Fee
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

        [SwaggerOperation(Summary = "This endpoint use for get all Fees with Details")]
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var FeesDetails = await _unitOfWork.Fees.All();
                return Ok(FeesDetails);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for get Fees details")]
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> GetFees(int id)
        {
            try
            {
                var fees = await _unitOfWork.Fees.GetById(id);

                if (fees == null)
                    return NotFound();

                var student = await _unitOfWork.Student.GetStudent(fees.StuId);

                var feesModel = new FeesViewModel
                {
                    Id = fees.Id,
                    FeesType = fees.FeesType,
                    FeesDescription = fees.FeesDescription,
                    AmountofFees = fees.AmountofFees,
                    RecieptNo = fees.RecieptNo,
                    studName = student.FullName,
                    StuId = fees.StuId,
                    studRegNo = student.StudRegNo
                };

                return Ok(feesModel);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [SwaggerOperation(Summary = "This endpoint use for Add Fees")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] FeesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var fees = _mapper.Map<FeesModel, Fees>(model);

                    await _unitOfWork.Fees.Add(fees);
                    await _unitOfWork.CompleteAsync();

                    return Ok(fees);
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
