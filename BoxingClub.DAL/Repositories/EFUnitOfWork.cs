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
        private BoxingGroupRepository _boxingGroupRepository;

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

        public IRepository<BoxingGroup> BoxingGroups
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

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}


