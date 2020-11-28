using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Database;
using backend.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backend
{
    public class Program
    {

        //For database Migration purposes, Add-Migration, Update-database, than run backend with async Main to migrate the Roles and Admin

        public static async Task Main(string[] args)
        {
            var host = await (CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<WebstoreDbContext>());

            host.Run();
        }

        /*public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }*/

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
