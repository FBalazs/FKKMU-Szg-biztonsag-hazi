using System;
using backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Database
{
    public class WebstoreDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<UserFile> UserFiles { get; set; }

        public WebstoreDbContext()
        {

        }

        public WebstoreDbContext(DbContextOptions<WebstoreDbContext> options) : base(options)
        {

        }
    }
}
