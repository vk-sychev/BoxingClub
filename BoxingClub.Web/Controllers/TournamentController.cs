using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.Models;
using HttpClientAdapters.Interfaces;
using HttpClients.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuthorizeRoles = BoxingClub.Web.CustomAttributes.AuthorizeRolesAttribute;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    [Route("[controller]")]
    public class TournamentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITournamentService _tournamentService;
        private readonly ITournamentClientAdapter _tournamentClientAdapter;

        public TournamentController(IMapper mapper,
                                    ITournamentService tournamentService,
                                    ITournamentClientAdapter tournamentClientAdapter)
        {
            _mapper = mapper;
            _tournamentService = tournamentService;
            _tournamentClientAdapter = tournamentClientAdapter;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTournaments()
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.GetTournaments(token);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedTournaments = _mapper.Map<List<TournamentViewModel>>(response.Items);
            return View(mappedTournaments);
        }


        [HttpGet("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> EditTournament(int id)
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.GetTournament(token, id);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedTournament = _mapper.Map<TournamentViewModel>(response.Item);
            return View(mappedTournament);
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> EditTournament(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedModel = _mapper.Map<TournamentModel>(model);
                var response = await _tournamentClientAdapter.EditTournament(token, mappedModel);

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            return View(model);
        }

        [HttpGet("[action]")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public IActionResult CreateTournament()
        {
            return View();
        }

        [HttpPost("[action]")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> CreateTournament(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedModel = _mapper.Map<TournamentModel>(model);

                var response = await _tournamentClientAdapter.CreateTournament(token, mappedModel);

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            return View(model);
        }

        [Route("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.DeleteTournament(token, id);

            var redirect = GetRedirectAction(response);
            if (redirect != null)
            {
                return redirect;
            }

            return RedirectToAction("GetAllTournaments", "Tournament");
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsTournament(int id)
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.GetTournament(token, id);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedTournament = _mapper.Map<TournamentViewModel>(response.Item);

            return View(mappedTournament);
        }

        private IActionResult GetRedirectAction(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            return null;
        }
    }
}
