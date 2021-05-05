using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BoxingClub.DAL.EF
{
    public class BoxingClubContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<BoxingGroup> BoxingGroups { get; set; }

        public DbSet<FighterExperienceSpecification> FighterExperienceSpecifications { get; set; }

        public BoxingClubContext(DbContextOptions<BoxingClubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
         }
    }
}
