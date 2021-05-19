using AutoMapper;
using BoxingClub.BLL.Implementation.Services;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Implementation.Implementation;
using BoxingClub.DAL.Interfaces;
using BoxingClub.DAL.Repositories;
using BoxingClub.Web.Mapping;
using BoxingClub.Web.Models;
using BoxingClub.Web.Validations;
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
using System;
using System.Collections.Generic;

namespace BoxingClub.Web
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

            var passwordConfig = Configuration.GetSection("PasswordSettings");

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = Convert.ToInt32(passwordConfig.GetSection("RequiredLength").Value);
                options.Password.RequireNonAlphanumeric = Convert.ToBoolean(passwordConfig.GetSection("RequireNonAlphanumeric").Value);
                options.Password.RequireUppercase = Convert.ToBoolean(passwordConfig.GetSection("RequireUppercase").Value);
                options.Password.RequireLowercase = Convert.ToBoolean(passwordConfig.GetSection("RequireLowercase").Value);
            })
            .AddEntityFrameworkStores<BoxingClubContext>();

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddTransient<IStudentSpecification, FighterExperienceSpecification>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddTransient<IRoleProvider, RoleProvider>();
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();

            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IBoxingGroupService, BoxingGroupService>();
            services.AddTransient<IMedicalCertificateService, MedicalCertificateService>();
            services.AddTransient<ITournamentService, TournamentService>();

            var mapperProfiles = new List<Profile>() { new BoxingGroupProfile(), new ResultProfile(), new RoleProfile(), new StudentProfile(), 
                                                       new UserProfile(), new MedicalCertificateProfile(), new TournamentProfile(),
                                                       new AgeCategoryProfile(), new WeightCategoryProfile(), new CategoryProfile(),
                                                       new AgeWeightCategoryProfile(), new TournamentRequirementProfile()};
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            //mapperConfig.AssertConfigurationIsValid();

            services.AddAutoMapper(typeof(BoxingGroupProfile), typeof(ResultProfile), typeof(RoleProfile), typeof(StudentProfile), 
                                   typeof(UserProfile), typeof(MedicalCertificateProfile), typeof(TournamentProfile),
                                   typeof(AgeCategoryProfile), typeof(WeightCategoryProfile), typeof(CategoryProfile),
                                   typeof(AgeWeightCategoryProfile), typeof(TournamentRequirementProfile));

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddFluentValidation();

            services.AddTransient<IValidator<SignUpViewModel>, SignUpViewModelValidator>();
            services.AddTransient<IValidator<SignInViewModel>, SignInViewModelValidator>();
            services.AddTransient<IValidator<StudentFullViewModel>, StudentFullViewModelValidator>();
            services.AddTransient<IValidator<UserViewModel>, UserViewModelValidator>();
            services.AddTransient<IValidator<BoxingGroupLiteViewModel>, BoxingGroupLiteViewModelValidator>();
            services.AddTransient<IValidator<MedicalCertificateViewModel>, MedicalCertificateViewModelValidator>();

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
