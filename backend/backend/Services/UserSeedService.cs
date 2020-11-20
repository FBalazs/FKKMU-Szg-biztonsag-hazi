using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class UserSeedService : IUserSeedService
    {
        private readonly UserManager<User> _userManager;

        public UserSeedService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedUserAsync()
        {
            if (!(await _userManager.GetUsersInRoleAsync(Roles.Roles.Admin)).Any())
            {
                var user = new User
                {
                    UserName = "admin@webstore.com",
                    Email = "admin@webstore.com",
                    Role = nameof(Roles.Roles.Admin),
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var createResult = await _userManager.CreateAsync(user, "Asd123+");
                var addToRoleResult = await _userManager.AddToRoleAsync(user, Roles.Roles.Admin);

                if (!createResult.Succeeded || !addToRoleResult.Succeeded)
                {
                    throw new ApplicationException($"Administrator could not be created: " +
                        $"{string.Join(", ", createResult.Errors.Concat(addToRoleResult.Errors).Select(e => e.Description))}");
                }
            }
        }
    }
}
