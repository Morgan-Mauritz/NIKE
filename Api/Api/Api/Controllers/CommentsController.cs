﻿using Api.Exceptions;
using Api.Model;
using Api.Services.CommentService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> PostComment([FromBody]AddCommentDTO comment, [FromHeader] string apiKey) 
        {
            try
            {
                return Ok(new Response<CommentWithUserDTO>(await _service.PostComment(comment, apiKey))); 
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, ex.Message));
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> DeleteComment(int id, [FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<CommentDTO>(await _service.DeleteComment(id, apiKey)));
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new Response<NotFoundException>(Status.Fail, ex.Message));
            }
        }
        [HttpPut()]
        [ProducesResponseType(typeof(int?), 200)] 
        public async Task<IActionResult> UpdateComment([FromBody] EditComment comment, [FromHeader] string apiKey)
        {
            try
            {
                return Ok(new Response<CommentDTO>(await _service.UpdateComment(comment, apiKey)));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new Response<UnauthorizedAccessException>(Status.Fail, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new Response<NotFoundException>(Status.Fail, ex.Message));
            }
        }
    }
}
