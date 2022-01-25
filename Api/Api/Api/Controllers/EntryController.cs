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
        public async Task<IActionResult> SetEntry([FromBody] AddEntry entryDto)
        {
            try
            {
                return Ok(await _service.SetEntry(entryDto));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }       
        }

        [HttpPut(":id")]
        public async Task<IActionResult> UpdateEntry([FromBody] UpdateEntry updateDto, long id)
        {
            try
            {
                return Ok(await _service.UpdateEntry(updateDto, id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete(":id")]
        public async Task<IActionResult> RemoveEntry(long id)
        {
            try
            {
                return Ok(await _service.DeleteEntry(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}
