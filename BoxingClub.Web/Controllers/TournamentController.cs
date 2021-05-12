using AutoMapper;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            var mappedTournament = _mapper.Map<TournamentViewModel>(tournament);
            return View(mappedTournament);
        }


        [HttpGet]
        [Route("Tournament/CreateTournament")]
        public async Task<IActionResult> CreateTournament()
        {
            //categories
            return View();
        }


    }
}
