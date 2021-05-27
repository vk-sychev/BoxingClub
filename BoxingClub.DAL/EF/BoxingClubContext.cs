using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BoxingClub.DAL.EF
{
    public class BoxingClubContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
                                     IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
                                     IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<BoxingGroup> BoxingGroups { get; set; }

        public DbSet<MedicalCertificate> MedicalCertificates { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<TournamentRequest> TournamentRequests { get; set; }

        public BoxingClubContext(DbContextOptions<BoxingClubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(k => k.Id);

                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.HasKey(k => k.Id);

                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Students)
                .WithMany(s => s.Tournaments)

                .UsingEntity<TournamentRequest>(
                    j => j.HasOne(tr => tr.Student)
                        .WithMany(s => s.TournamentRequests)
                        .HasForeignKey(tr => tr.StudentId),

                    j => j.HasOne(tr => tr.Tournament)
                        .WithMany(t => t.TournamentRequests)
                        .HasForeignKey(tr => tr.TournamentId),

                    j => j.HasKey(tr => tr.Id)
                );
            
            modelBuilder.Seed();
        }
    }
}
