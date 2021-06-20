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
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Infrastructure.Constants;
using HttpClientAdapters.Interfaces;
using System.Net;
using HttpClients.Models;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.CoachRoleName, Constants.ManagerRoleName)]
    [Route("[controller]")]
    public class AcceptedTournamentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITournamentClientAdapter _tournamentClientAdapter;

        public AcceptedTournamentController(IMapper mapper, 
                                            ITournamentClientAdapter tournamentClientAdapter)
        {
            _mapper = mapper;
            _tournamentClientAdapter = tournamentClientAdapter;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAcceptedTournaments()
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.GetAcceptedTournaments(token);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedTournaments = _mapper.Map<List<TournamentViewModel>>(response.Items);
            return View(mappedTournaments);
        }

        [HttpGet("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> ParticipateInTournament(int tournamentId)
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.ParticipateInTournament(token, tournamentId);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var model = _mapper.Map<TournamentRequestViewModel>(response.Item);
            return View(model);
        }

        [HttpPost("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> ParticipateInTournament(TournamentRequestViewModel model)
        {
            var token = Request.Cookies["token"];

            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<TournamentRequestModel>(model);
                var response = await _tournamentClientAdapter.ParticipateInTournament(token, mappedModel);

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }
                return RedirectToAction("GetAllTournaments", "Tournament");
            }

            if (!model.Students.Any())
            {
                ModelState.AddModelError("studentSelectionError", ErrorConstants.ValidationErrorMessageForStudentSelection);
            }
            else
            {
                ModelState.AddModelError("generalError", ErrorConstants.GeneralError);
            }


            var resp = await _tournamentClientAdapter.ParticipateInTournament(token, model.TournamentId);

            var redir = GetRedirectAction(resp.StatusCode);
            if (redir != null)
            {
                return redir;
            }

            model = _mapper.Map<TournamentRequestViewModel>(resp.Item);
            return View("ParticipateInTournament", model);
        }

        [HttpGet("[action]/{tournamentId}")]
        public async Task<IActionResult> DetailsAcceptedTournament(int tournamentId)
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.GetTournamentRequest(token, tournamentId);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var model = _mapper.Map<TournamentRequestViewModel>(response.Item);
            return View(model);
        }

        [HttpPost("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> DetailsAcceptedTournament(TournamentRequestViewModel model)
        {
            var token = Request.Cookies["token"]; 
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<TournamentRequestModel>(model);
                var response = await _tournamentClientAdapter.EditAcceptedTournament(token, mappedModel);

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("GetAcceptedTournaments");
            }

            if (!model.Students.Any())
            {
                ModelState.AddModelError("studentSelectionError", ErrorConstants.ValidationErrorMessageForStudentSelection);
            }
            else
            {
                ModelState.AddModelError("generalError", ErrorConstants.GeneralError);
            }


            var resp = await _tournamentClientAdapter.ParticipateInTournament(token, model.TournamentId);

            var redir = GetRedirectAction(resp.StatusCode);
            if (redir != null)
            {
                return redir;
            }

            model = _mapper.Map<TournamentRequestViewModel>(resp.Item);
            return View(model);
        }

        [HttpDelete("{tournamentId}")]
        [Route("[action]/{tournamentId}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> DeleteTournamentRequest(int tournamentId)
        {
            var token = Request.Cookies["token"];
            var response = await _tournamentClientAdapter.DeleteTournamentRequest(token, tournamentId);

            var redirect = GetRedirectAction(response);
            if (redirect != null)
            {
                return redirect;
            }

            return RedirectToAction("GetAcceptedTournaments");
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
