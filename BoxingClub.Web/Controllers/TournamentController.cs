﻿using AutoMapper;
using BoxingClub.BLL.DomainEntities;
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
            var mappedTournaments = _mapper.Map<List<TournamentLiteViewModel>>(tournaments);
            return View(mappedTournaments);
        }


        [HttpGet]
        [Route("Tournament/EditTournament/{id}")]
        public async Task<IActionResult> EditTournament(int? id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            var mappedTournament = _mapper.Map<TournamentFullViewModel>(tournament);
            var editModel = new CreateEditTournamentViewModel()
            {
                Tournament = mappedTournament,
                Categories = SetSelectedCategories(await GetCategories(), mappedTournament.Categories)
            };
            return View(editModel);
        }

        [HttpPost]
        [Route("Tournament/EditTournament/{id}")]
        public async Task<IActionResult> EditTournament(CreateEditTournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Tournament.Categories = GetSelectedCategories(model.Categories);
                var tournament = _mapper.Map<TournamentFullDTO>(model.Tournament);
                await _tournamentService.UpdateTournamentAsync(tournament);
                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            return View(model);
        }

        [HttpGet]
        [Route("Tournament/CreateTournament")]
        public async Task<IActionResult> CreateTournament()
        {
            var createModel = new CreateEditTournamentViewModel()
            {
                Categories = await GetCategories()
            };
            return View(createModel);
        }

        [HttpPost]
        [Route("Tournament/CreateTournament")]
        public async Task<IActionResult> CreateTournament(CreateEditTournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Tournament.Categories = GetSelectedCategories(model.Categories);
                var tournament = _mapper.Map<TournamentFullDTO>(model.Tournament);
                await _tournamentService.CreateTournamentAsync(tournament);
                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            return View(model);
        }


        private List<CategoryViewModel> GetSelectedCategories(List<CategoryViewModel> categories)
        {
            List<CategoryViewModel> selectedCategories = new List<CategoryViewModel>();

            foreach(var item in categories)
            {
                if (item.IsSelected)
                {
                    selectedCategories.Add(item);
                }
            }

            return selectedCategories;
        }


        private List<CategoryViewModel> SetSelectedCategories(List<CategoryViewModel> allCategories, List<CategoryViewModel> selectedCategories)
        {
            foreach (var item in selectedCategories)
            {
                var category = allCategories.FirstOrDefault(x => x.Id == item.Id);
                if (item.Id == category.Id)
                {
                    category.IsSelected = true;
                }
            }
            return allCategories;
        }

        private async Task<List<CategoryViewModel>> GetCategories()
        {
            var categories = await _tournamentService.GetCategories();
            var mappedCategories = _mapper.Map<List<CategoryViewModel>>(categories);
            return mappedCategories;
        }
    }
}
