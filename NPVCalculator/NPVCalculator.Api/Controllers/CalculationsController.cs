using System;
using Microsoft.AspNetCore.Mvc;
using NPVCalculator.Api.Repositories;

namespace NPVCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        
        public CalculationsController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        [HttpGet]
        public IActionResult Get() 
        {
            var calculations = _uow.Calculations.GetAll();
            return Ok(calculations);
        }

        [HttpGet("parameter/{parameterId}")]
        public IActionResult GetByParameterId(long parameterId)
        {
            var calculations = _uow.Calculations.GetByParameterId(parameterId);
            return Ok(calculations);
        }
    }
}