using Api.Model;
using Api.Services.POIServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class POIController : Controller
    {
        private readonly IPOIService _service;
        public POIController(IPOIService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPOI([FromQuery] double longitude, [FromQuery] double latitude)
        {
            return Ok(new Response<POIDto>(await _service.GetPOI(longitude, latitude)));
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPOIList([FromQuery] FilterPOI filterPOI)
        {
            return Ok(new Response<List<POIDto>>(await _service.GetPOIList(filterPOI)));
        }

        [HttpPost]
        public async Task<IActionResult> SetPOI([FromBody] POIDto poiDto)
        {
            return Ok(new Response<POIDto>(await _service.SetPOI(poiDto)));
        }





    }




}
