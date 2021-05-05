﻿using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }

        IBoxingGroupRepository BoxingGroups { get; }

        IFighterExperienceSpecificationRepository FighterExperienceSpecifications { get; }

        Task SaveAsync();
    }
}
