using Api.Services.ForecastServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
