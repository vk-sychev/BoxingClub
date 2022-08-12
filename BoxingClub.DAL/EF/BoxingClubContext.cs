using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BoxingClub.DAL.EF
{
    public class BoxingClubContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<TournamentRequest> TournamentRequests { get; set; }

        public BoxingClubContext(DbContextOptions<BoxingClubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
