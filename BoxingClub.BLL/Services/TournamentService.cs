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

        public async Task CreateTournamentAsync(TournamentFullDTO tournamentDTO)
        {
            if (tournamentDTO == null)
            {
                throw new ArgumentNullException(nameof(tournamentDTO), "Tournament is null");
            }

            var tournament = _mapper.Map<Tournament>(tournamentDTO);
            /*tournament.Categories = await GetSelectedCategories(tournament.Categories);*/

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

        public async Task<TournamentFullDTO> GetTournamentByIdAsync(int? id)
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

            var mappedTournament = _mapper.Map<TournamentFullDTO>(tournament);
            return mappedTournament;
        }

        public async Task UpdateTournamentAsync(TournamentFullDTO tournamentDTO)
        {
            if (tournamentDTO == null)
            {
                throw new ArgumentNullException(nameof(tournamentDTO), "Tournament is null");
            }

            var tournament = _mapper.Map<Tournament>(tournamentDTO);

            var tournamentFromDb = await _database.Tournaments.GetByIdAsync(tournament.Id);

            if (tournamentFromDb == null)
            {
                throw new NotFoundException($"Tournament with id = {tournament.Id} isn't found", "");
            }

            Tournament.UpdateTournamentProperties(tournamentFromDb, tournament);
            await UpdateTournamentCategories(tournamentFromDb, tournament);

            _database.Tournaments.Update(tournamentFromDb);
            await _database.SaveAsync();
        }

        public async Task<List<TournamentLiteDTO>> GetTournamentsAsync()
        {
            var tournaments = await _database.Tournaments.GetAllAsync();
            var mappedTournaments = _mapper.Map<List<TournamentLiteDTO>>(tournaments);
            return mappedTournaments;
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            var categories = await _database.Categories.GetAllAsync();
            var mappedCategories = _mapper.Map<List<CategoryDTO>>(categories);
            return mappedCategories;
        }

        private async Task UpdateTournamentCategories(Tournament tournamentFromDb, Tournament updatedTournament)
        {
            var oldCategories = tournamentFromDb.Categories;
            /*var newCategories = await GetSelectedCategories(updatedTournament.Categories);*/
            var newCategories = updatedTournament.Categories;
            
            if (newCategories.Count == 0)
            {
                return;
            }

            List<Category> deleteCategories = new List<Category>();
            List<Category> addCategories = new List<Category>();
            List<Category> foundCategories = new List<Category>();

            foreach (var item in newCategories)
            {
                var category = oldCategories.FirstOrDefault(x => x.Id == item.Id);

                if (category == null)
                {
                    addCategories.Add(item);
                }
                else
                {
                    foundCategories.Add(category);
                }
            }

            deleteCategories = oldCategories.Except(foundCategories).ToList();
            if (deleteCategories.Count != 0)
            {
                foreach(var item in deleteCategories)
                {
                    tournamentFromDb.Categories.Remove(item);
                }
            }

            if (addCategories.Count!=0)
            {
                foreach (var item in addCategories)
                {
                    tournamentFromDb.Categories.Add(item);
                }
            }
        }

/*        private async Task<List<Category>> GetSelectedCategories(List<Category> categoryIds)
        {
            List<Category> selectedCategories = new List<Category>();
            var categories = await _database.Categories.GetAllAsync();
            foreach (var item in categoryIds)
            {
                var category = categories.FirstOrDefault(x => x.Id == item.Id);
                selectedCategories.Add(category);
            }
            return selectedCategories;
        }*/
    }
}
