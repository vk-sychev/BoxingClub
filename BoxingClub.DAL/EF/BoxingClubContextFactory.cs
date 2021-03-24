using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace BoxingClub.DAL.EF
{
    public class BoxingClubContextFactory : IDesignTimeDbContextFactory<BoxingClubContext>
    {
        public BoxingClubContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BoxingClubContext>();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("BoxingClubDB");
            optionsBuilder.UseSqlServer(connectionString);
            return new BoxingClubContext(optionsBuilder.Options);
        }
    }
}
