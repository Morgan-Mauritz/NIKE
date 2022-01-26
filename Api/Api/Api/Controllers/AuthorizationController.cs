using System;
using System.Net;
using System.Threading.Tasks;
using Api.Model;
using Api.Services.AuthorizationServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService _authorizationservice;

        public AuthorizationController(IAuthorizationService authorizationservice)
        {
            _authorizationservice = authorizationservice;
        }

        /// <summary>
        /// Log in to application using username and password
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "email": "admin@nike.com",
        ///        "password": "admin123",
        ///     }
        ///
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(Response<UserApiDto>), 200)]
        public async Task<IActionResult> Login([FromBody] LogInModel loginModel)
        {
            try
            {
                return Ok(new Response<UserApiDto>(await _authorizationservice.SignIn(loginModel)));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, ex.Message));
            }
        }
    }
}
