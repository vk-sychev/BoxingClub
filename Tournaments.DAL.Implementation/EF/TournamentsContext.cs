using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.DAL.Entities;

namespace Tournaments.DAL.Implementation.EF
{
    public class TournamentsContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<TournamentRequest> TournamentRequests { get; set; }

        public TournamentsContext(DbContextOptions<TournamentsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
