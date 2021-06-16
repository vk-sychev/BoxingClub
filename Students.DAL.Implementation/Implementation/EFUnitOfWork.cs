using System.Threading.Tasks;
using Students.DAL.Implementation.EF;
using Students.DAL.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.DAL.Implementation.Implementation
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly StudentsContext _db;
        private IStudentRepository _studentRepository;
        private IBoxingGroupRepository _boxingGroupRepository;
        private IMedicalCertificateRepository _medicalCertificateRepository;

        public EFUnitOfWork(StudentsContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
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

        public IMedicalCertificateRepository MedicalCertificates
        {
            get
            {
                if (_medicalCertificateRepository == null)
                {
                    _medicalCertificateRepository = new MedicalCertificateRepository(_db);
                }

                return _medicalCertificateRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}


