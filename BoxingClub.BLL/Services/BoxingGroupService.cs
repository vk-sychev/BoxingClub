using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Interfaces;
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
    }
}
