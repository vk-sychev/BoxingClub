﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            return View(mappedStudents);
        }

        [HttpDelete("{id}")]
        [Route("AcceptedTournament/DeleteFromTournament/{studentId}")]
        public async Task<IActionResult> DeleteFromTournament(int? studentId, int? tournamentId, List<StudentFullViewModel> students)
        {
            return View();
        }
    }
}
