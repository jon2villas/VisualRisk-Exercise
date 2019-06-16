using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPVCalculator.Web.Models;

namespace NPVCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calculate([FromBody] Parameter parameter)
        {
            var client = _httpClientFactory.CreateClient("NPVCalculatorApi");

            var response = await client.PostAsJsonAsync("calculator/calculate", parameter);
            if (response.IsSuccessStatusCode) 
            {
                var result = await response.Content.ReadAsAsync<List<Calculation>>();
                var parameterId = result.FirstOrDefault()?.ParameterId ?? 0;
                return Ok(parameterId);
            }
            
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Calculations(long parameterId)
        {
            var client = _httpClientFactory.CreateClient("NPVCalculatorApi");

            var jsonLastParameter = await client.GetStringAsync("parameters/last");
            if (string.IsNullOrEmpty(jsonLastParameter))
            {
                return Ok(new { LastParameterId = 0, Calculations = new List<Calculation>() });
            }

            var lastParameter = JsonConvert.DeserializeObject<Parameter>(jsonLastParameter);
            parameterId = (parameterId <= 0 || parameterId > lastParameter.Id)
                ? lastParameter.Id
                : parameterId;

            var jsonCalculations = await client.GetStringAsync($"calculations/parameter/{parameterId}");
            if (string.IsNullOrEmpty(jsonCalculations)) 
            {
                return Ok(new { LastParameterId = lastParameter.Id, Calculations = new List<Calculation>() });
            }

            var calculations = JsonConvert.DeserializeObject<List<Calculation>>(jsonCalculations);

            return Ok(new { LastParameterId = lastParameter.Id, Calculations = calculations });
        }

        private List<Parameter> GetDisplayParameters(List<Parameter> parameters, Parameter selected)
        {
            var lastId = parameters.Last().Id;
            var startId = selected.Id - 3;
            var endId = selected.Id + 3;

            if (startId <= 0) 
            {
                startId = 1;
                endId = 7;
            }

            if (endId > lastId) 
            {
                endId = lastId;
                startId = (lastId - 6) > 0 ? lastId - 6 : 1;
            }

            if (startId == 2) 
            {
                startId = startId - 1;
                endId = endId - 1;
            }

            if (endId == (lastId - 1)) 
            {
                startId = startId + 1;
                endId = endId + 1;
            }

            return parameters
                .Where(p => p.Id >= startId && p.Id <= endId)
                .ToList();
        }
    }
}
