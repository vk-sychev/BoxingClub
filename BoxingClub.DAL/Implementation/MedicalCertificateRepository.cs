using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class MedicalCertificateRepository : IMedicalCertificateRepository
    {
        private readonly BoxingClubContext _db;

        public MedicalCertificateRepository(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(MedicalCertificate item)
        {
            var student = await _db.Students.FindAsync(item.StudentId);
            item.Student = student;
            await _db.MedicalCertificates.AddAsync(item);
        }

        public void Delete(MedicalCertificate item)
        {
            //валидация
            _db.MedicalCertificates.Remove(item);
        }

        public async Task<IEnumerable<MedicalCertificate>> GetAllAsync()
        {
            return await _db.MedicalCertificates.AsQueryable().ToListAsync();
        }

        public async Task<List<MedicalCertificate>> GetAllByStudentIdAsync(int studentId)
        {
            return await _db.MedicalCertificates.AsQueryable().Where(s => s.Id == studentId).ToListAsync();
        }

        public async Task<MedicalCertificate> GetByIdAsync(int id)
        {
            return await _db.MedicalCertificates.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(MedicalCertificate item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
