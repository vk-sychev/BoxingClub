using BoxingClub.Infrastructure.Policies;
using HttpClientAdapters.Implementation;
using HttpClientAdapters.Interfaces;
using HttpClients.Implementation;
using HttpClients.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.BLL.Implementation.HttpSpecificationClient;
using Tournaments.BLL.Implementation.Services;
using Tournaments.BLL.Interfaces;
using Tournaments.BLL.Interfaces.HttpSpecificationClient;
using Tournaments.DAL.Implementation.EF;
using Tournaments.DAL.Implementation.Implementation;
using Tournaments.DAL.Interfaces;

namespace Tournaments.API
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
            services.AddDbContext<TournamentsContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TournamentsDB")));

            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.RequireHttpsMetadata = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true

                };
                config.SaveToken = true;
                config.Authority = "https://localhost:10001";
            });

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddTransient<ITournamentService, TournamentService>();
            services.AddTransient<IStudentSelectionService, StudentSelectionService>();

            services.AddTransient<ISpecificationClient, SpecificationHttpClientAdapter>();
            services.AddHttpClient<ISpecificationHttpClient, SpecificationHttpClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("SpecServer").GetSection("Uri").Value);
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("SpecServer").GetSection("HttpClientTimeout").Value));
            })
                .AddPolicyHandler(SpecServerPolicy.GetWaitAndRetryPolicy())
                .AddPolicyHandler(SpecServerPolicy.GetTimeoutPolicy());

            services.AddHttpClient<IStudentClient, StudentClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Students.API").GetSection("Uri").Value);
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("Students.API")
                    .GetSection("HttpClientTimeout").Value));
            })
                .AddPolicyHandler(APIServersPolicy.GetWaitAndRetryPolicy())
                .AddPolicyHandler(APIServersPolicy.GetTimeoutPolicy());

            services.AddTransient<IStudentClientAdapter, StudentClientAdapter>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tournaments.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tournaments.API v1"));
            }

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
