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
                if (_studentRepository == null) 
                {
                    _studentRepository = new StudentRepository(db);
                }
                return _studentRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}


