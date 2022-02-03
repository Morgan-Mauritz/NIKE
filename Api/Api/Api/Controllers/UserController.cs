using System;
using System.Net;
using System.Threading.Tasks;
using Api.Model;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /user
        ///     {
        ///        "firstName": "Admin",
        ///        "lastName": "Nike",
        ///        "email": "admin@nike.com",
        ///        "username": "admin",
        ///        "password": "admin123
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Response<UserDto>), 200)]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto userDto)
        {
            return Ok(new Response<UserDto>(await _userService.AddUser(userDto)));
        }
        /// <summary>
        /// Updates an existing users properties. Omitt properties which should not be updated.
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /user
        ///     {
        ///        "firstName": "Admin",
        ///        "lastName": "Nike",
        ///        "email": "admin@nike.com",
        ///        "username": "admin",
        ///        "password": "admin123
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(Response<UserDto>), 200)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, [FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<UserDto>(await _userService.UpdateUser(updateUserDto, apiKey)));
            }
            catch (UnauthorizedAccessException ex)
            {
#if RELEASE
                return StatusCode((int)HttpStatusCode.Unauthorized,new Response<UnauthorizedAccessException>(Status.Fail, ex.Message));
#endif
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, ex.Message, ex));
            }
        }
        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Response<UserDto>), 200)]
        public async Task<IActionResult> RemoveUser([FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<UserDto>(await _userService.RemoveUser(apiKey)));
            }
            catch (UnauthorizedAccessException ex)
            {
#if RELEASE
                return StatusCode((int)HttpStatusCode.Unauthorized,new Response<UnauthorizedAccessException>(Status.Fail, ex.Message));
#endif
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, ex.Message, ex));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<UserDto>), 200)]
        public async Task<IActionResult> GetUser([FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<UserDto>(await _userService.GetUser(apiKey)));
            }
            catch (UnauthorizedAccessException ex)
            {

                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, ex.Message, ex));
            }
        }
    }
}
