using Microsoft.AspNetCore.Identity;
using System;
namespace backend.Entities
{
    public class User : IdentityUser<int>, IEntityBase
    {
        public User()
        {
        }
    }
}
