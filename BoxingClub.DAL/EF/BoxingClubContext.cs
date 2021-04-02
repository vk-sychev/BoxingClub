using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.EF
{
    public class BoxingClubContext : IdentityDbContext
    {
        public DbSet<Student> Students { get; set; }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

        public BoxingClubContext(DbContextOptions<BoxingClubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
