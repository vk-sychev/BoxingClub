using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Student> Students { get; }
        Task Save();
    }
}
