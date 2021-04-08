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
    public class CoachService : ICoachService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;

        public CoachService(IMapper mapper,
                            IUnitOfWork uow)
        {
            _mapper = mapper;
            _database = uow;
        }

        public async Task<List<CoachDTO>> GetCoaches()
        {
            var coaches = await _database.Coaches.GetAll();
            var coacheDTOs = _mapper.Map<List<CoachDTO>>(coaches);
            return coacheDTOs;
        }

        public async Task<CoachDTO> GetCoach(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Coach's id is null");
            }
            var coach = await _database.Coaches.Get(id.Value);
            if (coach == null)
            {
                throw new NotFoundException($"Coach with id = {id.Value} isn't found", "");
            }
            return _mapper.Map<CoachDTO>(coach);
        }

        public Task UpdateCoach(CoachDTO coachDTO)
        {
            if (coachDTO == null)
            {
                throw new ArgumentNullException(nameof(coachDTO), "Coach is null");
            }
            var coach = _mapper.Map<Coach>(coachDTO);
            _database.Coaches.Update(coach);
            return _database.Save();
        }

        public async Task DeleteCoach(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Coach's id is null");
            }
            var res = await _database.Coaches.Delete(id.Value);
            if (!res)
            {
                throw new NotFoundException($"Coach with id = {id.Value} isn't found", "");
            }
            await _database.Save();
        }
    }
}
