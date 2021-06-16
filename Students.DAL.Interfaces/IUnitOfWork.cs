using System.Threading.Tasks;

namespace Students.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }

        IBoxingGroupRepository BoxingGroups { get; }

        IMedicalCertificateRepository MedicalCertificates { get; } 

        Task SaveAsync();
    }
}
