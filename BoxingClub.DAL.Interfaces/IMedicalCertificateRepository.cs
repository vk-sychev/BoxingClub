using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IMedicalCertificateRepository : IRepository<MedicalCertificate>
    {
        Task<List<MedicalCertificate>> GetAllByStudentIdAsync(int studentId);
    }
}
