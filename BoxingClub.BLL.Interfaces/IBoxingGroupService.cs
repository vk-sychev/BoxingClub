﻿using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IBoxingGroupService
    {
        Task CreateBoxingGroupAsync(BoxingGroupDTO groupDTO);

        Task DeleleBoxingGroupAsync(int? id);

        Task<BoxingGroupDTO> GetBoxingGroupByIdAsync(int? id);

        Task<List<BoxingGroupDTO>> GetBoxingGroupsAsync();

        Task<List<BoxingGroupDTO>> GetBoxingGroupsByCoachIdAsync(string coachId);

        Task UpdateBoxingGroupAsync(BoxingGroupDTO groupDTO);

        Task<BoxingGroupDTO> GetBoxingGroupWithStudentsByIdAsync(int? id);
    }
}