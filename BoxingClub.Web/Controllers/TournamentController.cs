using AutoMapper;
using BoxingClub.BLL.Interfaces;
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
    [Authorize]
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
        [Route("Tournament/EditTournament/{id}")]
        public async Task<IActionResult> EditTournament(int? id)
        {
            ViewBag.AgeCategories = await GetAgeCategories();
            ViewBag.WeightCategories = await GetWeightCategories();

            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            var mappedTournament = _mapper.Map<TournamentViewModel>(tournament);
            return View(mappedTournament);
        }


        [HttpGet]
        [Route("Tournament/CreateTournament")]
        public async Task<IActionResult> CreateTournament()
        {
            ViewBag.AgeCategories = await GetAgeCategories();
            ViewBag.WeightCategories = await GetWeightCategories();
            
            return View();
        }

        [HttpPost]
        [Route("Tournament/CreateTournament")]
        public async Task<IActionResult> CreateTournament(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            ViewBag.AgeCategories = await GetAgeCategories();
            ViewBag.WeightCategories = await GetWeightCategories();

            return View(model);
        }

        private async Task<List<AgeCategoryViewModel>> GetAgeCategories()
        {
            var ageCategories = await _tournamentService.GetAgeCategories();
            var mappedAgeCategories = _mapper.Map<List<AgeCategoryViewModel>>(ageCategories);
            return mappedAgeCategories;
        }

        private async Task<List<WeightCategoryViewModel>> GetWeightCategories()
        {
            var weightCategories = await _tournamentService.GetWeightCategories();
            var mappedWeightCategories = _mapper.Map<List<WeightCategoryViewModel>>(weightCategories);
            return mappedWeightCategories;
        }
    }
}
