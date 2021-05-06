using BoxingClub.BLL.DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IMedicalCertificateService
    {
        Task CreateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO);

        Task DeleteMedicalCertificateAsync(int? id);

        Task<MedicalCertificateDTO> GetMedicalCertificateByIdAsync(int? id);

        Task<List<MedicalCertificateDTO>> GetMedicalCertificatesAsync();

        Task UpdateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO);

        Task<List<MedicalCertificateDTO>> GetMedicalCertificatesByStudentIdAsync(int? studentId);

        Task<MedicalCertificateDTO> GetLastMedicalCertificateByStudentIdAsync(int? studentId);
    }
}