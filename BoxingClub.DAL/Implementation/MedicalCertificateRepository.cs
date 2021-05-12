﻿using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

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

        public Task<List<MedicalCertificate>> GetMedicalCertificatesByStudentIdAsync(int studentId)
        {
            return _db.MedicalCertificates.AsQueryable().Where(s => s.StudentId == studentId).ToListAsync();
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
