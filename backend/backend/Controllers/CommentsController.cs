﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult getAllComment([FromBody] int animation_id)
        {
            var result = _commentService.GetAllComment(animation_id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> uploadComment([FromBody] Comment comment)
        {
            await _commentService.SaveComment(comment);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> deleteComment([FromBody] int comment_id)
        {
            await _commentService.DeleteComment(comment_id);

            return Ok();
        }
    }
}