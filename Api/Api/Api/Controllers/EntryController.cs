using Api.Model;
using Api.Services.EntryServices;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(await _service.SetEntry(entryDto));
        }




    }
}
