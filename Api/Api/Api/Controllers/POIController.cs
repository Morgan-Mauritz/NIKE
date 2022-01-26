using Api.Model;
using Api.Services.POIServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetPOI([FromQuery] double longitude, [FromQuery] double latitude, [FromQuery] string name)
        {
            return Ok(new Response<POIDto>(await _service.GetPOI(longitude, latitude, name)));
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPOIList([FromQuery] FilterPOI filterPOI)
        {

            //offset = 5 , amount = 10 => prevoffset = 5  


            var nextOffset = filterPOI.Offset + filterPOI.Amount;
            var prevOffset = filterPOI.Offset - filterPOI.Amount;
            var httpString = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host ;

            if (prevOffset < 0)
            {
                prevOffset = 0;
            }

            var result = await _service.GetPOIList(filterPOI); 
            var nextPage = httpString + $"poi/list?offset={nextOffset}&amount={filterPOI.Amount}&sort={filterPOI.Sort}&city={filterPOI.City}&country={filterPOI.Country}&name={filterPOI.Name}";
            var prevPage = httpString + $"poi/list?offset={prevOffset}&amount={filterPOI.Amount}&sort={filterPOI.Sort}&city={filterPOI.City}&country={filterPOI.Country}&name={filterPOI.Name}";
            
           

            if (filterPOI.Offset == 0)
            {
                
                prevPage = null;
            }

            if (nextOffset >= result.total)
            {
                nextPage = null; 
            }
            return Ok(new PaginationResponse<List<POIDto>>(result.poiList, filterPOI.Offset, filterPOI.Amount, nextPage, prevPage, result.total));
        }

        [HttpPost]
        public async Task<IActionResult> SetPOI([FromBody] POIDto poiDto, [FromHeader] string apiKey)
        {
            return Ok(new Response<POIDto>(await _service.SetPOI(poiDto, apiKey)));
        }
    }
}
