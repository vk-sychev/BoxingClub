using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.DAL.Entities;
using Students.DAL.Implementation.EF;
using Students.DAL.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.DAL.Implementation.Implementation
{
    class StudentRepository : IStudentRepository
    {
        private readonly StudentsContext _db;

        public StudentRepository(StudentsContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Student is null");
            }
            await _db.Students.AddAsync(item);
        }

        public void Delete(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Student is null");
            }
            _db.Students.Remove(item);
        }

        public Task<Student> GetByIdAsync(int id)
        {
            return _db.Students.Include(x => x.BoxingGroup)
                               .Include(x => x.MedicalCertificates)
                               .SingleOrDefaultAsync(s => s.Id == id);
        }


        public Task<List<Student>> GetAllAsync()
        {
            return _db.Students.Include(x => x.BoxingGroup)
                               .Include(x => x.MedicalCertificates).ToListAsync();
        }

        public void Update(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Student is null");
            }
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
