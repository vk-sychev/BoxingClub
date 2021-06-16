using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.DAL.Entities;

namespace Students.DAL.Implementation.EF
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<BoxingGroup> BoxingGroups { get; set; }

        public DbSet<MedicalCertificate> MedicalCertificates { get; set; }

        public StudentsContext(DbContextOptions<StudentsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }
    }
}
