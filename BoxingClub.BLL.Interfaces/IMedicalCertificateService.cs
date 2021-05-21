using BoxingClub.BLL.DomainEntities;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IMedicalCertificateService
    {
        Task CreateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO);

        Task DeleteMedicalCertificateAsync(int? id);

        Task<MedicalCertificateDTO> GetMedicalCertificateByIdAsync(int? id);

        Task UpdateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO);

    }
}