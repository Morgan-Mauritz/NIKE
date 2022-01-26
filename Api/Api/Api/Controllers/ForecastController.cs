using System.Threading.Tasks;
using Api.Model;
using Api.Services.ForecastServices;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Gets a weather forecast based on long and lat coordinates
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<ForecastDto>), 200)]
        public async Task<IActionResult> GetForecast([FromQuery] double longitude, [FromQuery] double latitude)
        {
            return Ok(new Response<ForecastDto>(await _service.GetForecast(longitude, latitude)));
        }

    }
}
