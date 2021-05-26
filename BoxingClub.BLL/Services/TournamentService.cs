using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;

        public TournamentService(IUnitOfWork uow,
                                 IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
        }

        public async Task CreateTournamentAsync(TournamentDTO tournamentDTO)
        {
            if (tournamentDTO == null)
            {
                throw new ArgumentNullException(nameof(tournamentDTO), "Tournament is null");
            }

            var tournament = _mapper.Map<Tournament>(tournamentDTO);

            await _database.Tournaments.CreateAsync(tournament);
            await _database.SaveAsync();
        }

        public async Task DeleteTournamentAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Tournaments's id is null");
            }

            var tournament = await _database.Tournaments.GetByIdAsync(id.Value);

            if (tournament == null)
            {
                throw new NotFoundException($"Tournament with id = {id.Value} isn't found", "");
            }

            _database.Tournaments.Delete(tournament);
            await _database.SaveAsync();
        }

        public async Task<TournamentDTO> GetTournamentByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Tournaments's id is null");
            }

            var tournament = await _database.Tournaments.GetByIdAsync(id.Value);

            if (tournament == null)
            {
                throw new NotFoundException($"Tournament with id = {id.Value} isn't found", "");
            }

            var mappedTournament = _mapper.Map<TournamentDTO>(tournament);
            return mappedTournament;
        }

        public async Task UpdateTournamentAsync(TournamentDTO tournamentDTO)
        {
            if (tournamentDTO == null)
            {
                throw new ArgumentNullException(nameof(tournamentDTO), "Tournament is null");
            }

            var tournament = _mapper.Map<Tournament>(tournamentDTO);

            _database.Tournaments.Update(tournament);
            await _database.SaveAsync();
        }

        public async Task<List<TournamentDTO>> GetTournamentsAsync()
        {
            var tournaments = await _database.Tournaments.GetAllAsync();
            var mappedTournaments = _mapper.Map<List<TournamentDTO>>(tournaments);
            return mappedTournaments;
        }
    }
}
