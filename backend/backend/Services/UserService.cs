using backend.Entities;
using backend.Exceptions;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole<int>> _roleManager;

        private readonly IDbRepository _repository;

        private readonly AppSettings _appSettings;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IDbRepository repository, IOptions<AppSettings> appSettings)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._repository = repository;
            this._appSettings = appSettings.Value;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _repository.FindAsync<User>(id);

            return user;
        }

        public IEnumerable<UserListDto> GetAll()
        {
            var users = _repository.GetAll<User>().Select(u => new UserListDto 
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            return users;
        }

        public async Task<UserDto> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);

            var roles = await _userManager.GetRolesAsync(user);

            if(roles == null)
            {
                return null;
            }

            if (result)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var userToken = tokenHandler.WriteToken(token);

                var userDto = new UserDto { Id = user.Id, Email = user.Email, Role = user.Role, Token = userToken };

                return userDto;
            }

            return null;
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new AppException("Password is required");
            }

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var addToRole = await _userManager.AddToRoleAsync(user, Roles.Roles.Customer);

                if (addToRole.Succeeded)
                {
                    user.Role = Roles.Roles.Customer;
                    await _userManager.UpdateAsync(user);
                }
            }

            return result;
        }

        //Change user Role
        public async Task<User> Update(int id, string role)
        {
            var user = await _repository.FindAsync<User>(id);

            if(user == null)
            {
                return null;
            }

            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (roleExists)
            {
                var roles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, roles);

                await _userManager.AddToRoleAsync(user, role);

                user.Role = role;
                await _userManager.UpdateAsync(user);

                return user;
            } 
            else
            {
                return null;
            }
        }

        public async Task<User> UpdatePassword(int id, string currentPassword, string password)
        {
            var user = await _repository.FindAsync<User>(id);

            if (user == null)
            {
                return null;
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, password);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new AppException(result.Errors.FirstOrDefault().Description);
            }
        }
    }
}
