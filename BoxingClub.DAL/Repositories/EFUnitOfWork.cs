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

        public EFUnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BoxingClubContext>();

            var options = optionsBuilder
            .UseSqlServer(connectionString)
            .Options;

            db = new BoxingClubContext(options);
        }

        public IRepository<Student> Students
        {
            get
            {
                if (_studentRepository != null)
                    _studentRepository = new StudentRepository(db);
                return _studentRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }


        //вопрос про _
        //не уверен, что кусок ниже мне нужен
        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

