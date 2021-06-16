using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.DAL.Entities;
using Students.DAL.Implementation.EF;
using Students.DAL.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.DAL.Implementation.Implementation
{
    public class MedicalCertificateRepository : IMedicalCertificateRepository
    {
        private readonly StudentsContext _db;

        public MedicalCertificateRepository(StudentsContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(MedicalCertificate item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Medical Certificate is null");
            }

            await _db.MedicalCertificates.AddAsync(item);
        }

        public void Delete(MedicalCertificate item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Medical Certificate is null");
            }
            _db.MedicalCertificates.Remove(item);
        }

        public Task<List<MedicalCertificate>> GetAllAsync()
        {
            return _db.MedicalCertificates.AsQueryable().ToListAsync();
        }

        public Task<MedicalCertificate> GetByIdAsync(int id)
        {
            return _db.MedicalCertificates.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(MedicalCertificate item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Medical Certificate is null");
            }
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
