using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.CustomAttributes;
using Tournaments.BLL.Interfaces;
using Tournaments.BLL.Entities;
using Tournaments.API.Models;

namespace Tournaments.API.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.CoachRoleName, Constants.ManagerRoleName)]
    [Route("[controller]")]
    public class AcceptedTournamentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudentSelectionService _studentSelectionService;
        private readonly ITournamentService _tournamentService;

        public AcceptedTournamentController(IMapper mapper, 
                                            IStudentSelectionService studentSelectionService,
                                            ITournamentService tournamentService)
        {
            _mapper = mapper;
            _studentSelectionService = studentSelectionService;
            _tournamentService = tournamentService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAcceptedTournaments()
        {
            var tournaments = await _tournamentService.GetAcceptedTournamentsAsync();
            return Ok(tournaments);
        }

        [HttpGet("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> ParticipateInTournament(int tournamentId)
        {
            var model = await GetPossibleTournamentRequestByTournamentId(tournamentId);
            return Ok(model);
        }

        [HttpPost("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> ParticipateInTournament([FromBody]TournamentRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedStudents = _mapper.Map<List<StudentFullDTO>>(model.Students);
                await _studentSelectionService.CreateTournamentRequest(model.TournamentId, mappedStudents);
                return Ok();
            }

            return BadRequest();
        }


        [HttpPost("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> EditAcceptedTournament([FromBody]TournamentRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedStudents = _mapper.Map<List<StudentFullDTO>>(model.Students);
                await _studentSelectionService.UpdateTournamentRequest(model.TournamentId, mappedStudents);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{tournamentId}")]
        [Route("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> DeleteTournamentRequest(int tournamentId)
        {
            await _studentSelectionService.DeleteTournamentRequest(tournamentId);
            return Ok();
        }

        [Route("[action]/{tournamentId}")]
        public async Task<TournamentRequestViewModel> GetTournamentRequestByTournamentId(int tournamentId)
        {
            var token = GetTokenFromRequest();
            var students = await _studentSelectionService.GetSelectedStudentsByTournamentId(token, tournamentId);
            return new TournamentRequestViewModel()
            {
                TournamentId = tournamentId,
                Students = students
            };
        }

        private async Task<TournamentRequestViewModel> GetPossibleTournamentRequestByTournamentId(int tournamentId)
        {
            var token = GetTokenFromRequest();
            var students = await _studentSelectionService.GetStudentsByTournamentId(token, tournamentId);
            return new TournamentRequestViewModel()
            {
                TournamentId = tournamentId,
                Students = students
            };
        }

        private string GetTokenFromRequest()
        {
            var header = Request.Headers["Authorization"];
            var token = header.ToString().Split(' ');
            return token[1];
        }
    }
}
