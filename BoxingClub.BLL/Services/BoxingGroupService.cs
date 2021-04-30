using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Services
{
    public class BoxingGroupService : IBoxingGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;

        public BoxingGroupService(IMapper mapper,
                                  IUnitOfWork uow)
        {
            _mapper = mapper;
            _database = uow;
        }

        public async Task<List<BoxingGroupDTO>> GetBoxingGroupsAsync()
        {
            var groups = await _database.BoxingGroups.GetAllAsync();
            var groupDTOs = _mapper.Map<List<BoxingGroupDTO>>(groups);
            return groupDTOs;
        }

        public async Task<BoxingGroupDTO> GetBoxingGroupByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Group's id is null");
            }
            var group = await _database.BoxingGroups.GetByIdAsync(id.Value);
            if (group == null)
            {
                throw new NotFoundException($"Group with id = {id.Value} isn't found", "");
            }
            return _mapper.Map<BoxingGroupDTO>(group);
        }

        public Task UpdateBoxingGroupAsync(BoxingGroupDTO groupDTO)
        {
            if (groupDTO == null)
            {
                throw new ArgumentNullException(nameof(groupDTO), "Group is null");
            }
            var group = _mapper.Map<BoxingGroup>(groupDTO);
            _database.BoxingGroups.Update(group);
            return _database.SaveAsync();
        }

        public async Task CreateBoxingGroupAsync(BoxingGroupDTO groupDTO)
        {
            if (groupDTO == null)
            {
                throw new ArgumentNullException(nameof(groupDTO), "Group is null");
            }
            var group = _mapper.Map<BoxingGroup>(groupDTO);
            await _database.BoxingGroups.CreateAsync(group);
            await _database.SaveAsync();
        }

        public async Task DeleleBoxingGroupAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Group's id is null");
            }
            var boxingGroup = await _database.BoxingGroups.GetByIdAsync(id.Value);           

            if (boxingGroup == null)
            {
                throw new NotFoundException($"Group with id = {id.Value} isn't found", "");
            }

            _database.BoxingGroups.Delete(boxingGroup);
            await _database.SaveAsync();
        }

        public async Task<BoxingGroupDTO> GetBoxingGroupWithStudentsByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Group's id is null");
            }
            var group = await _database.BoxingGroups.GetBoxingGroupWithStudentsByIdAsync(id.Value);
            if (group == null)
            {
                throw new NotFoundException($"Group with id = {id.Value} isn't found", "");
            }
            return _mapper.Map<BoxingGroupDTO>(group);
        }

        public async Task<List<BoxingGroupDTO>> GetBoxingGroupsByCoachIdAsync(string coachId)
        {
            if (string.IsNullOrEmpty(coachId))
            {
                throw new ArgumentNullException(nameof(coachId), "Coach's id is null");
            }
            var groups = await _database.BoxingGroups.GetBoxingGroupsByCoachIdAsync(coachId);
            var mappedGroups = _mapper.Map<List<BoxingGroupDTO>>(groups);
            return mappedGroups;
        }

        public async Task<PageModelDTO<BoxingGroupDTO>> GetBoxingGroupsPaginatedAsync(int pageIndex, int pageSize)
        {
            //validation
            var groups = await _database.BoxingGroups.GetBoxingGroupsPaginatedAsync(pageIndex, pageSize);
            var groupDTOs = _mapper.Map<List<BoxingGroupDTO>>(groups);
            var count = await _database.BoxingGroups.GetCountOfBoxingGroupsAsync();
            var model = new PageModelDTO<BoxingGroupDTO>() { Items = groupDTOs, Count = count };
            return model;
        }

        public async Task<PageModelDTO<BoxingGroupDTO>> GetBoxingGroupsByCoachIdPaginatedAsync(string id, int pageIndex, int pageSize)
        {
            var groups = await _database.BoxingGroups.GetBoxingGroupsByCoachIdPaginatedAsync(id, pageIndex, pageSize);
            var groupDTOs = _mapper.Map<List<BoxingGroupDTO>>(groups);
            var count = await _database.BoxingGroups.GetCountOfBoxingGroupsByCoachIdAsync(id);
            var model = new PageModelDTO<BoxingGroupDTO>() { Items = groupDTOs, Count = count };
            return model;
        }
    }
}
