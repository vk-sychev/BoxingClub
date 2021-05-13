using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }

        IBoxingGroupRepository BoxingGroups { get; }

        IMedicalCertificateRepository MedicalCertificates { get; }

        ITournamentRepository Tournaments { get; }

        ICategoryRepository Categories { get; }

        IAgeCategoryRepository AgeCategories { get; }

        IWeightCategoryRepository WeightCategories { get; }

        Task SaveAsync();
    }
}
