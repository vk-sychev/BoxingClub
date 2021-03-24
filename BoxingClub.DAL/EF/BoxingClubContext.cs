using BoxingClub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.EF
{
    public class BoxingClubContext : DbContext
    {
        //public string ConnectionString { get; set; }
        public DbSet<Student> Students { get; set; }

        public BoxingClubContext(DbContextOptions<BoxingClubContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
