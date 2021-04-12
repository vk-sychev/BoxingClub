using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }

        IBoxingGroupRepository BoxingGroups { get; }

        ICoachRepository Coaches { get; }

        Task SaveAsync();
    }
}
