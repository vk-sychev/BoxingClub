using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Student> Students { get; }

        IRepository<BoxingGroup> BoxingGroups { get; }
        Task Save();
    }
}
