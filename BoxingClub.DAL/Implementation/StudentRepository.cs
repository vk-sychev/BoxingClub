﻿using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Repositories
{
    class StudentRepository : IStudentRepository
    {
        private readonly BoxingClubContext _db;

        public StudentRepository(BoxingClubContext context)
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
            var res = _db.Students.Include(x => x.BoxingGroup).Include(x => x.MedicalCertificates).ToListAsync();
            return res;
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
