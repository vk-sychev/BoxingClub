using AutoMapper;
using BoxingClub.Web.Mapping;
using BoxingClub.Web.Models;
using BoxingClub.Web.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using BoxingClub.Infrastructure.Policies;
using HttpClientAdapters.Implementation;
using HttpClientAdapters.Interfaces;
using HttpClients.Implementation;
using HttpClients.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;

namespace BoxingClub.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IUserClient, UserClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("AuthServer").GetSection("Uri").Value);
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("AuthServer")
                    .GetSection("HttpClientTimeout").Value));
            })
               .AddPolicyHandler(AuthServerPolicy.GetWaitAndRetryPolicy())
               .AddPolicyHandler(AuthServerPolicy.GetTimeoutPolicy());

            services.AddHttpClient<IStudentClient, StudentClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Students.API").GetSection("Uri").Value);
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("Students.API")
                    .GetSection("HttpClientTimeout").Value));
            })
/*                .AddPolicyHandler(APIServersPolicy.GetWaitAndRetryPolicy())
                .AddPolicyHandler(APIServersPolicy.GetTimeoutPolicy())*/;

            services.AddHttpClient<ITournamentClient, TournamentClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Tournaments.API").GetSection("Uri").Value);
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("Students.API")
                    .GetSection("HttpClientTimeout").Value));
            })
                .AddPolicyHandler(APIServersPolicy.GetWaitAndRetryPolicy())
                .AddPolicyHandler(APIServersPolicy.GetTimeoutPolicy());


            services.AddTransient<ITournamentClientAdapter, TournamentClientAdapter>();
            services.AddTransient<IUserClientAdapter, UserClientAdapter>();
            services.AddTransient<IStudentClientAdapter, StudentClientAdapter>();

            var mapperProfiles = new List<Profile>() { new BoxingGroupProfile(), new RoleProfile(), new StudentProfile(),
                                                       new UserProfile(), new MedicalCertificateProfile(), new TournamentProfile(),
                                                       new TournamentRequestProfile()
            };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            //mapperConfig.AssertConfigurationIsValid();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                {
                    AuthenticationSchemes = new List<string>() { CookieAuthenticationDefaults.AuthenticationScheme }
                }
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddFluentValidation(configuration => configuration.ImplicitlyValidateChildProperties = true);

            services.AddTransient<IValidator<SignUpViewModel>, SignUpViewModelValidator>();
            services.AddTransient<IValidator<SignInViewModel>, SignInViewModelValidator>();
            services.AddTransient<IValidator<StudentFullViewModel>, StudentFullViewModelValidator>();
            services.AddTransient<IValidator<UserViewModel>, UserViewModelValidator>();
            services.AddTransient<IValidator<BoxingGroupLiteViewModel>, BoxingGroupLiteViewModelValidator>();
            services.AddTransient<IValidator<MedicalCertificateViewModel>, MedicalCertificateViewModelValidator>();
            services.AddTransient<IValidator<TournamentViewModel>, TournamentViewModelValidator>();
            services.AddTransient<IValidator<TournamentRequestViewModel>, TournamentRequestViewModelValidator>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/SignIn";
                    options.AccessDeniedPath = "/Administration/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Cookie", policy =>
                {
                    policy.AuthenticationSchemes = new List<string>()
                        {CookieAuthenticationDefaults.AuthenticationScheme};
                    policy.RequireAuthenticatedUser();
                });
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

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
            });

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
