using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Database;
using backend.Interfaces;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Entities.User, IdentityRole<int>>()
                .AddEntityFrameworkStores<WebstoreDbContext>()
                .AddDefaultTokenProviders();

            //DbContext
            //services.AddDbContext<WebstoreDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<WebstoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebstoreDbContext")));

            // Service
            services.AddScoped<IWebstoreService, WebstoreService>();
            services.AddScoped<IDbRepository, DbRepository<WebstoreDbContext>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILogService, LogService>();

            services.AddRouting();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
