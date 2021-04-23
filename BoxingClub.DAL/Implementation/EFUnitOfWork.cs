using BoxingClub.DAL.EF;
using BoxingClub.DAL.Interfaces;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BoxingClubContext _db;

        private IStudentRepository _studentRepository;
        private IBoxingGroupRepository _boxingGroupRepository;

        public EFUnitOfWork(BoxingClubContext context)
        {
            _db = context;
        }

        public IStudentRepository Students
        {
            get
            {
                if (_studentRepository == null) 
                {
                    _studentRepository = new StudentRepository(_db);
                }
                return _studentRepository;
            }
        }

        public IBoxingGroupRepository BoxingGroups
        {
            get
            {
                if (_boxingGroupRepository == null)
                {
                    _boxingGroupRepository = new BoxingGroupRepository(_db);
                }
                return _boxingGroupRepository;
            }
        }


        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}


