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

        [HttpGet]
        public async Task<IActionResult> GetForecast([FromQuery] double longitude, [FromQuery] double latitude)
        {
            return Ok(await _service.GetForecast(longitude, latitude));
        }
        
    }
}
