using AutoMapper;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Interfaces;
using BoxingClub.DAL.Repositories;
using BoxingClub.WEB.Controllers;
using BoxingClub.WEB.Mapping;
using BoxingClub.WEB.Models;
using BoxingClub.WEB.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
            services.AddDbContext<BoxingClubContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BoxingClubDB")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<BoxingClubContext>();

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountProvider, AccountProvider>();
            services.AddTransient<IBoxingGroupService, BoxingGroupService>();
            services.AddTransient<ICoachService, CoachService>();            

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            mapperConfig.AssertConfigurationIsValid();
            //IMapper mapper = mapperConfig.CreateMapper();

            services.AddAutoMapper(typeof(MappingProfile)); 

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddFluentValidation();

            services.AddTransient<IValidator<SignUpViewModel>, SignUpViewModelValidator>();
            services.AddTransient<IValidator<SignInViewModel>, SignInViewModelValidator>();
            services.AddTransient<IValidator<StudentFullViewModel>, StudentFullViewModelValidator>();
            services.AddTransient<IValidator<RoleViewModel>, RoleViewModelValidator>();
            services.AddTransient<IValidator<BoxingGroupLiteViewModel>, BoxingGroupLiteViewModelValidator>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Administration/AccessDenied";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
