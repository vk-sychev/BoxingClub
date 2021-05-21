using BoxingClub.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IMedicalCertificateRepository : IRepository<MedicalCertificate>
    {
        Task<List<MedicalCertificate>> GetMedicalCertificatesByStudentIdAsync(int studentId);
    }
}
