using Api.Exceptions;
using Api.Model;
using Api.Services.EntryServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryController : Controller
    {
        private readonly IEntryService _service;
        public EntryController(IEntryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SetEntry([FromBody] AddEntry entryDto, [FromHeader] string apiKey)
        {
            try
            {

                return Ok(await _service.SetEntry(entryDto, apiKey));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

        }

        [HttpPut(":id")]
        public async Task<IActionResult> UpdateEntry([FromBody] UpdateEntry updateDto, [FromHeader] string apiKey, long id)
        {
            try
            {
                return Ok(new Response<EntryDto>(await _service.UpdateEntry(updateDto, apiKey, id)));
            }
            catch (UnauthorizedAccessException ex)
            {
                #if RELEASE
                return StatusCode((int)HttpStatusCode.Unauthorized,new Response<UnauthorizedAccessException>(Status.Fail, (int)HttpStatusCode.Unauthorized, ex.Message));
                #endif
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, (int)HttpStatusCode.Unauthorized, ex.Message, ex));
            }
            catch (NotFoundException ex)
            {
#if RELEASE
                return StatusCode((int)HttpStatusCode.NotFound,new Response<NotFoundException>(Status.Fail, (int)HttpStatusCode.NotFound, ex.Message));
#endif
                return StatusCode((int)HttpStatusCode.NotFound, new Response<NotFoundException>(Status.Fail, (int)HttpStatusCode.NotFound, ex.Message, ex));
            }
        }

        [HttpDelete(":id")]
        public async Task<IActionResult> RemoveEntry(long id, [FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<EntryDto>(await _service.RemoveEntry(id, apiKey)));
            }
            catch (NotFoundException ex)
            {
#if RELEASE
                return StatusCode((int)HttpStatusCode.NotFound,new Response<NotFoundException>(Status.Fail, (int)HttpStatusCode.NotFound, ex.Message));
#endif
                return StatusCode((int)HttpStatusCode.NotFound, new Response<NotFoundException>(Status.Fail, (int)HttpStatusCode.NotFound, ex.Message, ex));
            }
        }
    }
}
