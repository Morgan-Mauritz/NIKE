using Api.Services.ForecastServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Api.Model.Forecast;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly IForceastService _service;
        public ForecastController(IForceastService service)
        {
            _service = service;
        }

        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<ForecastController> _logger;

        //public ForecastController(ILogger<ForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public async Task<IActionResult> GetForecast([FromQuery] double longitude, [FromQuery] double latitude)
        {
            return Ok(await _service.GetForecast(longitude, latitude));
        }
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
