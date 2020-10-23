using System;
using Microsoft.EntityFrameworkCore;

namespace backend.Database
{
    public class WebstoreDbContext : DbContext
    {
        public WebstoreDbContext()
        {

        }

        public WebstoreDbContext(DbContextOptions<WebstoreDbContext> options) : base(options)
        {

        }
    }
}
