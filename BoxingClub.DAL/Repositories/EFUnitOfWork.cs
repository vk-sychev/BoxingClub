using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BoxingClubContext _db;
        private StudentRepository _studentRepository;
        private IBoxingGroupRepository _boxingGroupRepository;
        private CoachRepository _coachRepository;

        public EFUnitOfWork(BoxingClubContext context)
        {
            _db = context;
        }

        public IRepository<Student> Students
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

        public IRepository<Coach> Coaches
        {
            get
            {
                if(_coachRepository == null)
                {
                    _coachRepository = new CoachRepository(_db);
                }
                return _coachRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}


