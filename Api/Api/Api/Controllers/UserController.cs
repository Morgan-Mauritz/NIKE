using Api.Model;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto userDto)
        {
            return Ok(new Response<UserDto>(await _userService.AddUser(userDto)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, [FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<UserDto>(await _userService.UpdateUser(updateUserDto, apiKey)));
            }
            catch (UnauthorizedAccessException ex)
            {
                #if RELEASE
                return StatusCode((int)HttpStatusCode.Unauthorized,new Response<UnauthorizedAccessException>(Status.Fail, (int)HttpStatusCode.Unauthorized, ex.Message));
                #endif
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, (int)HttpStatusCode.Unauthorized, ex.Message, ex));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<UserDto>(await _userService.RemoveUser(apiKey)));
            }
            catch (UnauthorizedAccessException ex)
            {
                #if RELEASE
                return StatusCode((int)HttpStatusCode.Unauthorized,new Response<UnauthorizedAccessException>(Status.Fail, (int)HttpStatusCode.Unauthorized, ex.Message));
                #endif
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, (int)HttpStatusCode.Unauthorized, ex.Message, ex));
            }
        }
    }
}
