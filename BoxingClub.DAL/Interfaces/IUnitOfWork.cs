using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Student> Students { get; }
        void Save();
    }
}
