﻿using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Student> Students { get; }

        IBoxingGroupRepository BoxingGroups { get; }

        IRepository<Coach> Coaches { get; }
        Task Save();
    }
}
