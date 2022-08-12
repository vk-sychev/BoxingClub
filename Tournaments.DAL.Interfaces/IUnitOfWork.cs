using System.Threading.Tasks;

namespace Tournaments.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ITournamentRepository Tournaments { get; }

        ITournamentRequestRepository TournamentRequests { get; }

        Task SaveAsync();
    }
}
