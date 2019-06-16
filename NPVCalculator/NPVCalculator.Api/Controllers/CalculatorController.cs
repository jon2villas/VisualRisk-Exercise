using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPVCalculator.Api.Models;
using NPVCalculator.Api.Repositories;

namespace NPVCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        
        public CalculatorController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] Parameter parameter) 
        {
            if (!(parameter.LowerBoundDiscountRate > 0 && parameter.LowerBoundDiscountRate <= 100) ||
                !(parameter.UpperBoundDiscountRate > 0 && parameter.UpperBoundDiscountRate <= 100) ||
                parameter.UpperBoundDiscountRate < parameter.LowerBoundDiscountRate ||
                !(parameter.DiscountRateIncrement >= 0 && parameter.DiscountRateIncrement <= 100) ||
                !parameter.CashFlows.Any())
            {
                return BadRequest("Invalid parameters supplied.");
            }

            _uow.Parameters.Add(parameter);

            var calculations = new List<Calculation>();
            var discountRate = 0D;

            while (discountRate < parameter.UpperBoundDiscountRate)
            {
                discountRate += (discountRate >= parameter.LowerBoundDiscountRate 
                    ? parameter.DiscountRateIncrement 
                    : parameter.LowerBoundDiscountRate);

                if (discountRate > parameter.UpperBoundDiscountRate)
                {
                    discountRate = parameter.UpperBoundDiscountRate;
                }
                
                var netPresentValue = GetNetPresentValue(discountRate, parameter.InitialValue, parameter.CashFlows.ToList());
                calculations.Add(new Calculation(parameter.Id, discountRate, netPresentValue));
            }

            _uow.Calculations.AddRange(calculations);

            await _uow.CompleteAsync();

            return Ok(calculations);
        }

        private static decimal GetNetPresentValue(double discountRate, decimal initialValue, IEnumerable<CashFlow> cashFlows) 
        {
            var npv = initialValue;
            
            npv += cashFlows.Select((cashFlow, index) => 
                cashFlow.Value / (decimal)Math.Pow((discountRate / 100) + 1, index + 1)).Sum();

            return npv;
        }
    }
}