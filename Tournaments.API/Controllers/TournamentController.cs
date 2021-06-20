using AutoMapper;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.BLL.Entities;
using Tournaments.BLL.Interfaces;

namespace Tournaments.API.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    [Route("[controller]")]
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTournaments()
        {
            var tournaments = await _tournamentService.GetTournamentsAsync();
            return Ok(tournaments);
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> EditTournament(TournamentDTO model)
        {
            if (ModelState.IsValid)
            {
                await _tournamentService.UpdateTournamentAsync(model);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("[action]")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> CreateTournament(TournamentDTO model)
        {
            if (ModelState.IsValid)
            {
                await _tournamentService.CreateTournamentAsync(model);
                return Ok();
            }

            return BadRequest();
        }

        [Route("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            await _tournamentService.DeleteTournamentAsync(id);
            return Ok();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            return Ok(tournament);
        }
    }
}
