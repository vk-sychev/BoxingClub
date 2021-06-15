﻿using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HttpClientAdapters.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using ArgumentException = BoxingClub.Infrastructure.Exceptions.ArgumentException;

namespace BoxingClub.BLL.Services
{
    public class BoxingGroupService : IBoxingGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly IUserClientAdapter _userClientAdapter;

        public BoxingGroupService(IMapper mapper,
                                  IUnitOfWork uow,
                                  IUserClientAdapter _userClientAdapter)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
            this._userClientAdapter = _userClientAdapter;
        }

        public async Task<List<BoxingGroupDTO>> GetBoxingGroupsAsync(string token)
        {
            var groups = await _database.BoxingGroups.GetAllAsync();
            var groupDTOs = _mapper.Map<List<BoxingGroupDTO>>(groups);

            var coaches = await GetCoaches(token);
            AssignCoachToGroups(groupDTOs, coaches);

            return groupDTOs;
        }

        public async Task<BoxingGroupDTO> GetBoxingGroupByIdAsync(int id, string token)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Group's id less or equal 0", nameof(id));
            }

            var group = await _database.BoxingGroups.GetByIdAsync(id);

            if (group == null)
            {
                throw new NotFoundException($"Group with id = {id} isn't found", "");
            }

            var mappedGroup = _mapper.Map<BoxingGroupDTO>(group);
            mappedGroup.Coach = await GetCoach(mappedGroup.CoachId, token);

            return mappedGroup;
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

        public async Task DeleteBoxingGroupAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Group's id less or equal 0", nameof(id));
            }
            var boxingGroup = await _database.BoxingGroups.GetByIdAsync(id);           

            if (boxingGroup == null)
            {
                throw new NotFoundException($"Group with id = {id} isn't found", "");
            }

            _database.BoxingGroups.Delete(boxingGroup);
            await _database.SaveAsync();
        }

        public async Task<BoxingGroupDTO> GetBoxingGroupWithStudentsByIdAsync(int id, string token)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Group's id less or equal 0", nameof(id));
            }

            var group = await _database.BoxingGroups.GetBoxingGroupWithStudentsByIdAsync(id);

            if (group == null)
            {
                throw new NotFoundException($"Group with id = {id} isn't found", "");
            }

            var mappedGroup = _mapper.Map<BoxingGroupDTO>(group);
            mappedGroup.Coach = await GetCoach(mappedGroup.CoachId, token);

            return mappedGroup;
        }

        public async Task<List<BoxingGroupDTO>> GetBoxingGroupsByCoachIdAsync(string coachId, string token)
        {
            if (string.IsNullOrEmpty(coachId))
            {
                throw new ArgumentNullException(nameof(coachId), "Coach's id is null");
            }
            var groups = await _database.BoxingGroups.GetBoxingGroupsByCoachIdAsync(coachId);
            var mappedGroups = _mapper.Map<List<BoxingGroupDTO>>(groups);

            var coaches = await GetCoaches(token);
            AssignCoachToGroups(mappedGroups, coaches);

            return mappedGroups;
        }

        public async Task<PageModelDTO<BoxingGroupDTO>> GetBoxingGroupsPaginatedAsync(SearchModelDTO searchDTO, string token)
        {
            if (searchDTO == null)
            {
                throw new ArgumentNullException(nameof(searchDTO), "SearchDTO is null");
            }

            if (searchDTO.PageIndex == null)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
            }

            if (searchDTO.PageSize == null)
            {
                searchDTO.PageSize = PageModelConstants.PageSize;
            }

            var groups = await _database.BoxingGroups.GetBoxingGroupsPaginatedAsync(searchDTO.PageIndex.Value, searchDTO.PageSize.Value);
            if (groups.Count == 0)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
                groups = await _database.BoxingGroups.GetBoxingGroupsPaginatedAsync(searchDTO.PageIndex.Value, searchDTO.PageSize.Value);
            }

            var groupDTOs = _mapper.Map<List<BoxingGroupDTO>>(groups);

            var coaches = await GetCoaches(token);
            AssignCoachToGroups(groupDTOs, coaches);

            var count = await _database.BoxingGroups.GetCountOfBoxingGroupsAsync();
            return new PageModelDTO<BoxingGroupDTO>() { Items = groupDTOs, Count = count };
        }

        public async Task<PageModelDTO<BoxingGroupDTO>> GetBoxingGroupsByCoachIdPaginatedAsync(string id, SearchModelDTO searchDTO, string token)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Coach id is null");
            }

            if (searchDTO == null)
            {
                throw new ArgumentNullException(nameof(searchDTO), "SearchDTO is null");
            }

            if (searchDTO.PageIndex == null)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
            }

            if (searchDTO.PageSize == null)
            {
                searchDTO.PageSize = PageModelConstants.PageSize;
            }

            var groups = await _database.BoxingGroups.GetBoxingGroupsByCoachIdPaginatedAsync(id, searchDTO.PageIndex.Value, searchDTO.PageSize.Value);
            if (groups.Count == 0)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
                groups = await _database.BoxingGroups.GetBoxingGroupsByCoachIdPaginatedAsync(id, searchDTO.PageIndex.Value, searchDTO.PageSize.Value);
            }

            var groupDTOs = _mapper.Map<List<BoxingGroupDTO>>(groups);

            var coaches = await GetCoaches(token);
            AssignCoachToGroups(groupDTOs, coaches);

            var count = await _database.BoxingGroups.GetCountOfBoxingGroupsByCoachIdAsync(id);
            var model = new PageModelDTO<BoxingGroupDTO>() { Items = groupDTOs, Count = count };
            return model;
        }

        private async Task<List<UserDTO>> GetCoaches(string token)
        {
            var response = await _userClientAdapter.GetUsersByRole(token, Constants.CoachRoleName);
            var coaches = new List<UserDTO>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var users = response.Users;
                coaches = _mapper.Map<List<UserDTO>>(users);
            }

            return coaches;
        }

        private async Task<UserDTO> GetCoach(string id, string token)
        {
            var response = await _userClientAdapter.GetUser(id, token);
            UserDTO coach = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var user = response.User;
                coach = _mapper.Map<UserDTO>(user);
            }

            return coach;
        }

        private void AssignCoachToGroups(List<BoxingGroupDTO> groups, List<UserDTO> coaches)
        {
            foreach (var group in groups)
            {
                group.Coach = coaches.FirstOrDefault(x => x.Id == group.CoachId);
            }
        }
    }
}
