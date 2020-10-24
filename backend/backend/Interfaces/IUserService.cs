using backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface IUserService
    {
        public void Register(User user);

        public void Login(User user, string password);

        public void Update(User user);
    }
}
