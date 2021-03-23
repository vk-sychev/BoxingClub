using Autofac;
using BoxingClub.BLL.Infrascructure;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Interfaces;
using BoxingClub.DAL.Repositories;
using BoxingClub.WEB.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB
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
            /*            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                            .AddAzureAD(options => Configuration.Bind("AzureAd", options));

                        services.AddControllersWithViews(options =>
                        {
                            var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                            options.Filters.Add(new AuthorizeFilter(policy));
                        });*/
            services.AddDbContext<BoxingClubContext>(options=>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BoxingClubDB")));

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddRazorPages();
        }


/*        public void ConfigureContainer(ContainerBuilder builder)
        {
            string connection = Configuration.GetConnectionString("BoxingClubDB");

            var optionsBuilder = new DbContextOptionsBuilder<BoxingClubContext>();
            var options = optionsBuilder
                .UseSqlServer(connection)
                .Options;


            builder.RegisterType<EFUnitOfWork>()
                .As<IUnitOfWork>()
                .WithParameter("options", new BoxingClubContext(options));

            builder.RegisterType<StudentService>()
                .As<IStudentService>();

            //var container = builder.Build();

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }*/


            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

/*            app.UseAuthentication();
            app.UseAuthorization();*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
