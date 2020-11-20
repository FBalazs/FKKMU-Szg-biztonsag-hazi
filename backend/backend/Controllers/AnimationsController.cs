using System;
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
    public class AnimationsController : ControllerBase
    {
        private readonly IFileService _animationService;

        public AnimationsController(IFileService animationService)
        {
            _animationService = animationService;
        }

        [HttpGet]
        public IActionResult getAllAnimation()
        {
            var result = _animationService.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> uploadAnimation([FromBody] byte[] file)
        {
            await _animationService.UploadFile(file);

            return Ok();
        }

        [Route("/{animation_id}")]
        [HttpGet]
        public async Task<IActionResult> downloadAnimation(int animation_id)
        {
            byte[] byteArray = await _animationService.DownloadFile(animation_id);

            if (byteArray != null)
                return new FileContentResult(byteArray, "application/octet-stream");

            return BadRequest(new { error = "File not found" });
        }

        [HttpDelete]
        public async Task<IActionResult> deleteAnimation([FromBody] int animation_id)
        {

            await _animationService.DeleteFile(animation_id);

            return Ok();
        }
    }
}
