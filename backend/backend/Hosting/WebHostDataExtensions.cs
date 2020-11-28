using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Interfaces;

namespace backend.Hosting
{
    public static class WebHostDataExtensions
    {
        public static async Task<IHost> MigrateDatabase<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider
                    .GetRequiredService<TContext>();
                context.Database.Migrate();

                var roleSeeder = serviceProvider
                    .GetRequiredService<IRoleSeedService>();
                await roleSeeder.SeedRoleAsync();

                var userSeeder = serviceProvider
                    .GetRequiredService<IUserSeedService>();
                await userSeeder.SeedUserAsync();
            }

            return host;
        }
    }
}
