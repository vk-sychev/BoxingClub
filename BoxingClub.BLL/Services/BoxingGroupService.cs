using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<BoxingGroupDTO>> GetBoxingGroups()
        {
            return _mapper.Map<List<BoxingGroupDTO>>(await _database.BoxingGroups.GetAll());
        }

        public async Task<BoxingGroupDTO> GetBoxingGroup(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Group's id is null");
            }
            var group = await _database.BoxingGroups.Get(id.Value);
            if (group == null)
            {
                throw new NotFoundException($"Group with id = {id.Value} isn't found", "");
            }
            return _mapper.Map<BoxingGroupDTO>(group);
        }

        public Task UpdateGroup(BoxingGroupDTO groupDTO)
        {
            if (groupDTO == null)
            {
                throw new ArgumentNullException(nameof(groupDTO), "Group is null");
            }
            var group = _mapper.Map<BoxingGroup>(groupDTO);
            _database.BoxingGroups.Update(group);
            return _database.Save();
        }

        public async Task CreateGroup(BoxingGroupDTO groupDTO)
        {
            if (groupDTO == null)
            {
                throw new ArgumentNullException(nameof(groupDTO), "Group is null");
            }
            var group = _mapper.Map<BoxingGroup>(groupDTO);
            await _database.BoxingGroups.Create(group);
            await _database.Save();
        }

        public async Task DeleleGroup(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Group's id is null");
            }
            if (!await _database.BoxingGroups.Delete(id.Value))
            {
                throw new NotFoundException($"Group with id = {id.Value} isn't found", "");
            }
            await _database.Save();
        }
    }
}
