using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BoxingClub.DAL.EF
{
    public class BoxingClubContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<BoxingGroup> BoxingGroups { get; set; }

        public DbSet<MedicalCertificate> MedicalCertificates { get; set; }

        public DbSet<AgeCategory> AgeCategories { get; set; }

        public DbSet<WeightCategory> WeightCategories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<AgeWeightCategory> AgeWeightCategories { get; set; }

        public DbSet<TournamentRequirement> TournamentRequirements { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }



        public BoxingClubContext(DbContextOptions<BoxingClubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
