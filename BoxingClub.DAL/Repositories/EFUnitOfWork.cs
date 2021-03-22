using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Repositories
{
    class EFUnitOfWork : IUnitOfWork
    {
        private BoxingClubContext db;
        private StudentRepository _studentRepository;

        public EFUnitOfWork(BoxingClubContext context)
        {
            db = context;
        }

        public IRepository<Student> Students
        {
            get
            {
                if (_studentRepository != null) {
                    _studentRepository = new StudentRepository(db);
                }
                return _studentRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

