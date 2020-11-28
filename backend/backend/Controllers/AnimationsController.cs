using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class AnimationsController : ControllerBase
    {
        private readonly IFileService _animationService;
        private readonly ILogService _logService;

        public AnimationsController(IFileService animationService, ILogService logService)
        {
            _animationService = animationService;
            _logService = logService;
        }

        [Authorize(Roles = Roles.Roles.Customer + "," + Roles.Roles.Admin)]
        [HttpGet]
        public IActionResult getAllAnimation()
        {
            var result = _animationService.GetAll();

            _logService.Logger(HttpContext.User.Identity.Name, "Összes animáció lekérése", "File");

            return Ok(result);
        }

        [Authorize(Roles = Roles.Roles.Customer + "," + Roles.Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> uploadAnimation([FromForm(Name = "file")] IFormFile file)
        {
            var result = await _animationService.UploadFile(file);

            _logService.Logger(HttpContext.User.Identity.Name, "Fájl feltöltése", "File", result.ToString());

            return Ok();
        }

        [Authorize(Roles = Roles.Roles.Customer + "," + Roles.Roles.Admin)]
        [HttpGet("{animation_id}")]
        public async Task<IActionResult> downloadAnimation(int animation_id)
        {
            byte[] byteArray = await _animationService.DownloadFile(animation_id);

            if (byteArray != null)
            {
                _logService.Logger(HttpContext.User.Identity.Name, "Fájl letöltése", "File", animation_id.ToString());
                return File(byteArray, "application/octet-stream");
            }

            return BadRequest(new { error = "File not found" });
        }

        [Authorize(Roles = Roles.Roles.Admin)]
        [HttpDelete("{animation_id}")]
        public async Task<IActionResult> deleteAnimation(int animation_id)
        {

            await _animationService.DeleteFile(animation_id);

            _logService.Logger(HttpContext.User.Identity.Name, "Fájl törlése", "File", animation_id.ToString());

            return Ok();
        }
    }
}
