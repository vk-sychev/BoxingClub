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
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.Infrastructure.Policies;
using HttpClientAdapters.Implementation;
using HttpClientAdapters.Interfaces;
using HttpClients.Implementation;
using HttpClients.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Students.API.Mapping;
using Students.API.WebManagers.Implementation;
using Students.API.WebManagers.Interfaces;
using Students.BLL.Implementation;
using Students.BLL.Interfaces;
using Students.DAL.Implementation.EF;
using Students.DAL.Implementation.Implementation;
using Students.DAL.Interfaces;

namespace Students.API
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
            services.AddDbContext<StudentsContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("StudentsDB")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromSeconds(20),
                        ValidateAudience = false
                    };
                    config.SaveToken = true;
                    config.Authority = "https://localhost:10001";
                    config.Audience = "https://localhost:10001";
                });

            services.AddAutoMapper(typeof(BoxingGroupProfile), typeof(StudentProfile), typeof(UserClientProfile), typeof(RoleProfile), typeof(UserProfile), typeof(MedicalCertificateProfile));

            var mapperProfiles = new List<Profile>() { new BoxingGroupProfile(), new RoleProfile(), new StudentProfile(), new UserProfile(), new MedicalCertificateProfile() };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            mapperConfig.AssertConfigurationIsValid();

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentSelectionService, StudentSelectionService>();

            services.AddScoped<IBoxingGroupService, BoxingGroupService>();
            services.AddScoped<IMedicalCertificateService, MedicalCertificateService>();

            services.AddScoped<IHomeWebManager, HomeWebManager>();
            services.AddScoped<IStudentWebManager, StudentWebManager>();

            services.AddHttpClient<IUserClient, UserClient>(client =>
                {
                    client.BaseAddress = new Uri(Configuration.GetSection("AuthServer").GetSection("Uri").Value);
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("AuthServer")
                        .GetSection("HttpClientTimeout").Value));
                })
                .AddPolicyHandler(AuthServerPolicy.GetWaitAndRetryPolicy())
                .AddPolicyHandler(AuthServerPolicy.GetTimeoutPolicy());

            services.AddScoped<IUserClientAdapter, UserClientAdapter>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Students.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students.API v1"));
            }

            app.Use(async (context, next) =>
            {
                var clone = Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;
                clone.NumberFormat.NumberDecimalSeparator = ".";

                Thread.CurrentThread.CurrentCulture = clone;
                Thread.CurrentThread.CurrentUICulture = clone;

                await next.Invoke();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
