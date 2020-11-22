using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;
using backend.Exceptions;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = Roles.Roles.Admin)]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        [Authorize(Roles = Roles.Roles.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetById(id);
            if(user == null)
            {
                return NotFound();
            }

            var userListDto = new UserListDto { Id = user.Id, Email = user.Email, Role = user.Role };

            return Ok(userListDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (String.IsNullOrEmpty(loginDto.Email) || String.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest(new { error = "Username or password is empty" });
            }

            var userDto = await _userService.Login(loginDto.Email, loginDto.Password);

            if (userDto == null)
            {
                return BadRequest(new { error = "Username or password is incorrect" });
            }

            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto loginDto)
        {
            var user = new User { UserName = loginDto.Email, Email = loginDto.Email };

            try
            {
                var result = await _userService.Register(user, loginDto.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { error = "The password should be at least 6 long and contain capital letters, a number and a special character." });
                }
            }
            catch (AppException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize(Roles = Roles.Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateRoleDto upadateRoleDto)
        {
            var user = await _userService.Update(id, upadateRoleDto.Role);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize]
        [HttpPut("resetpassword/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePasswordDto updatePasswordDto)
        {
            var currentUserId = int.Parse(HttpContext.User.Identity.Name);
            if (currentUserId != id)
            {
                return Unauthorized();
            }

            if (String.IsNullOrEmpty(updatePasswordDto.CurrentPassword) || String.IsNullOrEmpty(updatePasswordDto.Password))
            {
                return BadRequest(new { error = "Old password and new password is needed." });
            }

            try
            {
                var user = await _userService.UpdatePassword(id, updatePasswordDto.CurrentPassword, updatePasswordDto.Password);
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (AppException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            return NoContent();
        }
    }
}
