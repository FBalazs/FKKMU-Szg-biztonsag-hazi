using backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class RoleSeedService : IRoleSeedService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleSeedService(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRoleAsync()
        {
            if (!await _roleManager.RoleExistsAsync(Roles.Roles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = Roles.Roles.Admin });
            }

            if (!await _roleManager.RoleExistsAsync(Roles.Roles.Customer))
            {
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = Roles.Roles.Customer });
            }
        }
    }
}
