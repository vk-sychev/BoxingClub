using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.BLL.Implementation;
using IdentityServer.BLL.Interfaces;
using IdentityServer.DAL.Entities;
using IdentityServer.DAL.Implementation.EF;
using IdentityServer.DAL.Implementation.Providers;
using IdentityServer.DAL.Interfaces;
using IdentityServer.Mapping;
using IdentityServer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
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
            var passwordConfig = Configuration.GetSection("PasswordSettings");

            services.AddDbContext<UserDbContext>(config =>
            {
                config.UseSqlServer(
                    Configuration.GetConnectionString("UsersDB"));
            })
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = Convert.ToInt32(passwordConfig.GetSection("RequiredLength").Value);
                options.Password.RequireNonAlphanumeric = Convert.ToBoolean(passwordConfig.GetSection("RequireNonAlphanumeric").Value);
                options.Password.RequireUppercase = Convert.ToBoolean(passwordConfig.GetSection("RequireUppercase").Value);
                options.Password.RequireLowercase = Convert.ToBoolean(passwordConfig.GetSection("RequireLowercase").Value);
            })
            .AddEntityFrameworkStores<UserDbContext>();

            services.AddIdentityServer(options =>
            {
                options.Authentication.CheckSessionCookieName = "IdentityServer.Cookies";
                options.Authentication.CookieLifetime = TimeSpan.FromSeconds(5);
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryClients(Config.GetClients())
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddProfileService<ProfileService>()
            .AddAspNetIdentity<ApplicationUser>();

            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IRoleProvider, RoleProvider>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
