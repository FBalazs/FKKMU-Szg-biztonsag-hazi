using System;
using backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Database
{
    public class WebstoreDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public WebstoreDbContext()
        {

        }

        public WebstoreDbContext(DbContextOptions<WebstoreDbContext> options) : base(options)
        {

        }
    }
}
