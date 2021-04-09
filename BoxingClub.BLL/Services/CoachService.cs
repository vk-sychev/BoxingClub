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

        public async Task<List<CoachDTO>> GetCoachesAsync()
        {
            var coaches = await _database.Coaches.GetAllAsync();
            var coachDTOs = _mapper.Map<List<CoachDTO>>(coaches);
            return coachDTOs;
        }

        public async Task<CoachDTO> GetCoachAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Coach's id is null");
            }
            var coach = await _database.Coaches.GetAsync(id.Value);
            if (coach == null)
            {
                throw new NotFoundException($"Coach with id = {id.Value} isn't found", "");
            }
            return _mapper.Map<CoachDTO>(coach);
        }

        public Task UpdateCoachAsync(CoachDTO coachDTO)
        {
            if (coachDTO == null)
            {
                throw new ArgumentNullException(nameof(coachDTO), "Coach is null");
            }
            var coach = _mapper.Map<Coach>(coachDTO);
            _database.Coaches.Update(coach);
            return _database.SaveAsync();
        }

        public async Task DeleteCoachAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Coach's id is null");
            }
            var res = await _database.Coaches.DeleteAsync(id.Value);
            if (!res)
            {
                throw new NotFoundException($"Coach with id = {id.Value} isn't found", "");
            }
            await _database.SaveAsync();
        }
    }
}
