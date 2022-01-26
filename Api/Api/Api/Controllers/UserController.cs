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
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            return Ok(new Response<UserDto>(await _userService.AddUser(userDto)));
        }
    }
}
