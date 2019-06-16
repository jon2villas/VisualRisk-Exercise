using System;
using Microsoft.AspNetCore.Mvc;
using NPVCalculator.Api.Repositories;

namespace NPVCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        
        public ParametersController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        [HttpGet]
        public IActionResult Get() 
        {
            var parameters = _uow.Parameters.GetAll();
            return Ok(parameters);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var parameter = _uow.Parameters.GetById(id);
            return Ok(parameter);
        }

        [HttpGet("last")]
        public IActionResult GetLast()
        {
            var parameter = _uow.Parameters.GetLast();
            return Ok(parameter);
        }
    }
}