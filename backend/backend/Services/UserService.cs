using backend.Entities;
using backend.Interfaces;
using backend.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole<int>> _roleManager;

        private readonly IDbRepository _repository;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IDbRepository repository)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._repository = repository;
        }

        public void Login(User user, string password)
        {
            throw new NotImplementedException();
        }

        public void Register(User user)
        {
            throw new NotImplementedException();
        }

        //Change user Role
        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
