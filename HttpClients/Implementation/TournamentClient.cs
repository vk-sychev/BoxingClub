using HttpClients.Interfaces;
using HttpClients.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace HttpClients.Implementation
{
    public class TournamentClient : ITournamentClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<UserClient> _logger;
        private readonly string _tournamentController = "Tournament";
        private readonly string _acceptedTournamentController = "AcceptedTournament";

        public TournamentClient(HttpClient httpClient,
                                ILogger<UserClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "httpClient is null");
            _baseUrl = httpClient.BaseAddress?.ToString();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "logger is null");
        }

        public async Task<HttpResponseMessage> CreateTournament(string token, TournamentModel model)
        {
            var createTournamentUrl = $"{_baseUrl}{_tournamentController}/CreateTournament";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(createTournamentUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> DeleteTournament(string token, int id)
        {
            var deleteTournamentUrl = $"{_baseUrl}{_tournamentController}/DeleteTournament/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteTournamentUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> EditTournament(string token, TournamentModel model)
        {
            var editTournamentUrl = $"{_baseUrl}{_tournamentController}/EditTournament/{model.Id}";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(editTournamentUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetTournament(string token, int id)
        {
            var getTournamentUrl = $"{_baseUrl}{_tournamentController}/GetTournament/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getTournamentUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetTournaments(string token)
        {
            var getTournamentsUrl = $"{_baseUrl}{_tournamentController}/GetTournaments";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getTournamentsUrl);

            return GetResponse(response);
        }



        public async Task<HttpResponseMessage> GetAcceptedTournaments(string token)
        {
            var getAcceptedTournamentsUrl = $"{_baseUrl}{_acceptedTournamentController}/GetAcceptedTournaments";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getAcceptedTournamentsUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> ParticipateInTournament(string token, int id)
        {
            var participateInTournamentUrl = $"{_baseUrl}{_acceptedTournamentController}/ParticipateInTournament/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(participateInTournamentUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> ParticipateInTournament(string token, TournamentRequestModel model)
        {
            var participateInTournamentUrl = $"{_baseUrl}{_acceptedTournamentController}/ParticipateInTournament/{model.TournamentId}";

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(participateInTournamentUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> EditAcceptedTournament(string token, TournamentRequestModel model)
        {
            var editAcceptedTournament = $"{_baseUrl}{_acceptedTournamentController}/EditAcceptedTournament/{model.TournamentId}";

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(editAcceptedTournament, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> DeleteTournamentRequest(string token, int tournamentId)
        {
            var deleteTournamentRequest = $"{_baseUrl}{_acceptedTournamentController}/DeleteTournamentRequest/{tournamentId}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(deleteTournamentRequest);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetTournamentRequest(string token, int tournamentId)
        {
            var getTournamentsRequest = $"{_baseUrl}{_acceptedTournamentController}/GetTournamentRequestByTournamentId/{tournamentId}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getTournamentsRequest);

            return GetResponse(response);
        }


        private Dictionary<string, string> GetModelDictionary(object model)
        {
            var json = JsonConvert.SerializeObject(model);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        private HttpResponseMessage GetResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }
    }
}
