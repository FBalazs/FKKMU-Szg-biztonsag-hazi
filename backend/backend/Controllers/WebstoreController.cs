using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class WebstoreController : ControllerBase
    {
        private readonly IWebstoreService _service;

        public WebstoreController(IWebstoreService service)
        {
            this._service = service;
        }

        [Route("registrate")]
        [HttpPost]
        public IActionResult Registrate([FromBody] User user)
        {
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            return Ok();
        }

        [Route("upload")]
        [HttpPost]
        public IActionResult Upload()
        {
            return Ok();
        }

        [Route("download/{fileName}")]
        [HttpGet]
        public IActionResult Download(String fileName = "")
        {
            return Ok();
        }

    }
}
