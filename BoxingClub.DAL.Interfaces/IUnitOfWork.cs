using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ITournamentRepository Tournaments { get; }

        ITournamentRequestRepository TournamentRequests { get; }

        Task SaveAsync();
    }
}
