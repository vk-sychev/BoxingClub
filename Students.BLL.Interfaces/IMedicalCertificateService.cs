using System.Threading.Tasks;
using Students.BLL.DomainEntities;

namespace Students.BLL.Interfaces
{
    public interface IMedicalCertificateService
    {
        Task CreateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO);

        Task DeleteMedicalCertificateAsync(int id);

        Task<MedicalCertificateDTO> GetMedicalCertificateByIdAsync(int id);

        Task UpdateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO);

    }
}