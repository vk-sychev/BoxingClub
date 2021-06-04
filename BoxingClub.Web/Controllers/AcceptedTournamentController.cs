using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Web.Models;
using BoxingClub.Web.Helpers;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace BoxingClub.Web.Controllers
{
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

        public async Task<IActionResult> GetAcceptedTournaments()
        {
            var tournaments = await _tournamentService.GetAcceptedTournamentsAsync();
            var mappedTournaments = _mapper.Map<List<TournamentViewModel>>(tournaments);
            return View(mappedTournaments);
        }

        [HttpGet]
        [Route("AcceptedTournament/ParticipateInTournament/{tournamentId}")]
        public async Task<IActionResult> ParticipateInTournament(int tournamentId)
        {
            var model = await GetPossibleTournamentRequestByTournamentId(tournamentId);
            return View(model);
        }

        [HttpPost]
        [Route("AcceptedTournament/SaveParticipants")]
        public async Task<IActionResult> SaveParticipants(TournamentRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedStudents = _mapper.Map<List<StudentFullDTO>>(model.Students);
                await _studentSelectionService.CreateTournamentRequest(model.TournamentId, mappedStudents);
                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            var tournamentRequestModel = await GetPossibleTournamentRequestByTournamentId(model.TournamentId);

            ModelState.AddModelError("error", ErrorConstants.ValidationErrorMessage);
            return View("ParticipateInTournament", tournamentRequestModel);
        }

        [HttpGet]
        [Route("AcceptedTournament/DetailsAcceptedTournament/{tournamentId}")]
        public async Task<IActionResult> DetailsAcceptedTournament(int tournamentId)
        {
            var model = await GetTournamentRequestByTournamentId(tournamentId);
            return View(model);
        }

        [HttpPost]
        [Route("AcceptedTournament/DetailsAcceptedTournament/{tournamentId}")]
        public async Task<IActionResult> DetailsAcceptedTournament(TournamentRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedStudents = _mapper.Map<List<StudentFullDTO>>(model.Students);
                await _studentSelectionService.UpdateTournamentRequest(model.TournamentId, mappedStudents);
                return RedirectToAction("GetAcceptedTournaments");
            }

            ModelState.AddModelError("error", ErrorConstants.ValidationErrorMessage);
            var tournamentRequestModel = await GetTournamentRequestByTournamentId(model.TournamentId);
            return View(tournamentRequestModel);
        }

        [HttpDelete("{tournamentId}")]
        [Route("AcceptedTournament/DeleteTournamentRequest")]
        public async Task<IActionResult> DeleteTournamentRequest(int tournamentId)
        {
            await _studentSelectionService.DeleteTournamentRequest(tournamentId);
            return RedirectToAction("GetAcceptedTournaments");
        }

        private async Task<TournamentRequestViewModel> GetTournamentRequestByTournamentId(int tournamentId)
        {
            var students = await _studentSelectionService.GetSelectedStudentsByTournamentId(tournamentId);
            var mappedStudents = _mapper.Map<List<StudentFullViewModel>>(students);
            return new TournamentRequestViewModel()
            {
                TournamentId = tournamentId,
                Students = mappedStudents
            };
        }

        private async Task<TournamentRequestViewModel> GetPossibleTournamentRequestByTournamentId(int tournamentId)
        {
            var students = await _studentSelectionService.GetStudentsByTournamentId(tournamentId);
            var mappedStudents = _mapper.Map<List<StudentFullViewModel>>(students);
            return new TournamentRequestViewModel()
            {
                TournamentId = tournamentId,
                Students = mappedStudents
            };
        }
    }
}
