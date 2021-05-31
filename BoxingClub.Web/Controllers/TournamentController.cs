using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    public class TournamentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITournamentService _tournamentService;

        public TournamentController(IMapper mapper,
                                    ITournamentService tournamentService)
        {
            _mapper = mapper;
            _tournamentService = tournamentService;
        }

        [HttpGet]
        [Route("Tournament/GetAllTournaments")]
        public async Task<IActionResult> GetAllTournaments()
        {
            var tournaments = await _tournamentService.GetTournamentsAsync();
            var mappedTournaments = _mapper.Map<List<TournamentViewModel>>(tournaments);
            return View(mappedTournaments);
        }


        [HttpGet]
        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("Tournament/EditTournament/{id}")]
        public async Task<IActionResult> EditTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            var mappedTournament = _mapper.Map<TournamentViewModel>(tournament);
            return View(mappedTournament);
        }

        [HttpPost]
        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("Tournament/EditTournament/{id}")]
        public async Task<IActionResult> EditTournament(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tournament = _mapper.Map<TournamentDTO>(model);
                await _tournamentService.UpdateTournamentAsync(tournament);
                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            return View(model);
        }

        [HttpGet]
        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("Tournament/CreateTournament")]
        public IActionResult CreateTournament()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("Tournament/CreateTournament")]
        public async Task<IActionResult> CreateTournament(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tournament = _mapper.Map<TournamentDTO>(model);
                await _tournamentService.CreateTournamentAsync(tournament);
                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            return View(model);
        }

        [Route("Tournament/DeleteTournament/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            await _tournamentService.DeleteTournamentAsync(id);
            return RedirectToAction("GetAllTournaments", "Tournament");
        }

        [Route("Tournament/DetailsTournament/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [HttpGet]
        public async Task<IActionResult> DetailsTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            var mappedTournament = _mapper.Map<TournamentViewModel>(tournament);
            return View(mappedTournament);
        }

        private List<CategoryViewModel> SetSelectedCategories(List<CategoryViewModel> allCategories, List<CategoryViewModel> selectedCategories)
        {
            foreach (var item in selectedCategories)
            {
                var category = allCategories.FirstOrDefault(x => x.Id == item.Id);
                if (item.Id == category.Id)
                {
                    if (category != null)
                    {
                        category.IsSelected = true;
                    }
                }
            }
            return allCategories;
        }
    }
}
