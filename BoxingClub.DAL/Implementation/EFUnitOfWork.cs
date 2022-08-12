using BoxingClub.DAL.EF;
using BoxingClub.DAL.Implementation.Implementation;
using BoxingClub.DAL.Interfaces;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BoxingClubContext _db;
        private ITournamentRepository _tournamentRepository;
        private ITournamentRequestRepository _tournamentRequestRepository;

        public EFUnitOfWork(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public ITournamentRepository Tournaments
        {
            get
            {
                if (_tournamentRepository == null)
                {
                    _tournamentRepository = new TournamentRepository(_db);
                }

                return _tournamentRepository;
            }
        }

        public ITournamentRequestRepository TournamentRequests
        {
            get
            {
                if (_tournamentRequestRepository == null)
                {
                    _tournamentRequestRepository = new TournamentRequestRepository(_db);
                }

                return _tournamentRequestRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}


