using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Web.Models;

namespace BoxingClub.Web.Controllers
{
    public class AcceptedTournamentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudentSelectionService _studentSelectionService;

        public AcceptedTournamentController(IMapper mapper, 
                                            IStudentSelectionService studentSelectionService)
        {
            _mapper = mapper;
            _studentSelectionService = studentSelectionService;
        }

        public async Task<IActionResult> GetAcceptedTournaments()
        {
            return View();
        }

        [HttpGet]
        [Route("AcceptedTournament/ParticipateInTournament/{tournamentId}")]
        public async Task<IActionResult> ParticipateInTournament(int tournamentId)
        {
            var students = await _studentSelectionService.GetStudentsByTournamentId(tournamentId);
            if (students == null)
            {
                return View("StudentSelectionErrorView");
            }
            var mappedStudents = _mapper.Map<List<StudentFullViewModel>>(students);
            ViewBag.tournamentId = tournamentId;
            return View(mappedStudents);
        }

        [HttpPost]
        [Route("AcceptedTournament/SaveParticipants")]
        public async Task<IActionResult> SaveParticipants(int tournamentId, List<StudentFullViewModel> students)
        {
            if (ModelState.IsValid)
            {
                var mappedStudents = _mapper.Map<List<StudentFullDTO>>(students);
                await _studentSelectionService.CreateTournamentRequest(tournamentId, mappedStudents);
                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            return RedirectToAction("ParticipateInTournament", "AcceptedTournament", new {tournamentId = tournamentId});
        }
    }
}
