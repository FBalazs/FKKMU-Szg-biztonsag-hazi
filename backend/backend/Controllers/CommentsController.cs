using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogService _logService;

        public CommentsController(ICommentService commentService, ILogService logService)
        {
            _commentService = commentService;
            _logService = logService;
        }

        [Authorize(Roles = Roles.Roles.Customer + "," + Roles.Roles.Admin)]
        [HttpGet("{comment_id}")]
        public IActionResult getAllComment(int comment_id)
        {
            var result = _commentService.GetAllComment(comment_id);

            _logService.Logger(HttpContext.User.Identity.Name, "Összes komment lekérése", "Comment");

            return Ok(result);
        }

        [Authorize(Roles = Roles.Roles.Customer + "," + Roles.Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> uploadComment([FromBody] Comment comment)
        {
            await _commentService.SaveComment(comment);

            _logService.Logger(HttpContext.User.Identity.Name, "Új komment", "Comment", comment.Id.ToString());

            return Ok();
        }

        [Authorize(Roles = Roles.Roles.Admin)]
        [HttpDelete("{comment_id}")]
        public async Task<IActionResult> deleteComment(int comment_id)
        {
            await _commentService.DeleteComment(comment_id);

            _logService.Logger(HttpContext.User.Identity.Name, "Komment törlése", "Comment", comment_id.ToString());

            return Ok();
        }
    }
}
