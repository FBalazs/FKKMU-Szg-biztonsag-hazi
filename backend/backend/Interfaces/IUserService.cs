using backend.Entities;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Register(User user, string password);

        Task<UserDto> Login(string email, string password);

        Task<User> Update(int id, string role);

        Task<User> UpdatePassword(int id, string currentPassword, string password);

        Task<User> GetById(int id);

        Task<User> GetByEmail(string email);

        IEnumerable<UserListDto> GetAll();
    }
}
