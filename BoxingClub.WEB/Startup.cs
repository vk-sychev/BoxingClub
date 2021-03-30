using Autofac;
using AutoMapper;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Interfaces;
using BoxingClub.DAL.Repositories;
using BoxingClub.WEB.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<BoxingClubContext>();

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IStudentService, StudentService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddRazorPages();
        }


            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
                //app.UseStatusCodePagesWithReExecute("/Error{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            //app.U seAuthorization();

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
