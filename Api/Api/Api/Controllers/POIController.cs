using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model;
using Api.Services.POIServices;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Gets a poi based of the longitude and latitude
        /// 
        /// </summary>
        /// <param name="longitude" example="66.78988">Longitude of the POI</param>
        /// <param name="latitude" example="10.55774">Latitude of the POI</param>
        /// <param name="name" example="jannes+massage"> Enter space as "+"</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<POIDto>), 200)]
        public async Task<IActionResult> GetPOI([FromQuery] double longitude, [FromQuery] double latitude, [FromQuery] string name)
        {
            name = name.Replace("+", " ");
            return Ok(new Response<POIDto>(await _service.GetPOI(longitude, latitude, name)));
        }

        /// <summary>
        /// Gets a list of POI based on filter params
        /// </summary>
        /// <param name="filterPOI"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(PaginationResponse<List<POIDto>>), 200)]
        public async Task<IActionResult> GetPOIList([FromQuery] FilterPOI filterPOI)
        {

            var nextOffset = filterPOI.Offset + filterPOI.Amount;
            var prevOffset = filterPOI.Offset - filterPOI.Amount;
            var httpString = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;

            if (prevOffset < 0)
            {
                prevOffset = 0;
            }

            var result = await _service.GetPOIList(filterPOI);


            var nextPage = httpString + $"/poi/list?offset={nextOffset}&amount={filterPOI.Amount}&sort={filterPOI.Sort}&city={filterPOI.City}&country={filterPOI.Country}&name={filterPOI.Name}";
            var prevPage = httpString + $"/poi/list?offset={prevOffset}&amount={filterPOI.Amount}&sort={filterPOI.Sort}&city={filterPOI.City}&country={filterPOI.Country}&name={filterPOI.Name}";

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

        /// <summary>
        /// Posts a new POI based on POI properties
        /// </summary>
        /// <param name="poiDto"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<POIDto>), 200)]
        public async Task<IActionResult> SetPOI([FromBody] POIDto poiDto, [FromHeader] string apiKey)
        {
            return Ok(new Response<POIDto>(await _service.SetPOI(poiDto, apiKey)));
        }
    }
}
