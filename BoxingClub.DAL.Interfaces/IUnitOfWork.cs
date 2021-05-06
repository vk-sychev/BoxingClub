using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }

        IBoxingGroupRepository BoxingGroups { get; }

        IMedicalCertificateRepository MedicalCertificates { get; }

        Task SaveAsync();
    }
}
