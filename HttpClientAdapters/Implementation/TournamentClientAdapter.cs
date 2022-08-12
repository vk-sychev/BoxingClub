using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using HttpClients.Interfaces;
using HttpClients.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientAdapters.Implementation
{
    public class TournamentClientAdapter : ITournamentClientAdapter
    {
        private readonly ITournamentClient _tournamentClient;

        public TournamentClientAdapter(ITournamentClient tournamentClient)
        {
            _tournamentClient = tournamentClient;
        }

        public async Task<ItemResponseModel<TournamentModel>> GetTournament(string token, int id)
        {
            var response = await _tournamentClient.GetTournament(token, id);
            TournamentModel tournament = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                tournament = JsonConvert.DeserializeObject<TournamentModel>(content);
            }

            return new ItemResponseModel<TournamentModel>
            {
                StatusCode = response.StatusCode,
                Item = tournament
            };
        }

        public async Task<ItemsResponseModel<TournamentModel>> GetTournaments(string token)
        {
            var response = await _tournamentClient.GetTournaments(token);
            return await GetTournamentsFromResponse(response);
        }

        public async Task<HttpStatusCode> CreateTournament(string token, TournamentModel model)
        {
            var response = await _tournamentClient.CreateTournament(token, model);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteTournament(string token, int id)
        {
            var response = await _tournamentClient.DeleteTournament(token, id);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> EditTournament(string token, TournamentModel model)
        {
            var response = await _tournamentClient.EditTournament(token, model);
            return response.StatusCode;
        }

        public async Task<ItemsResponseModel<TournamentModel>> GetAcceptedTournaments(string token)
        {
            var response = await _tournamentClient.GetAcceptedTournaments(token);
            return await GetTournamentsFromResponse(response);
        }

        public async Task<HttpStatusCode> DeleteTournamentRequest(string token, int tournamentId)
        {
            var response = await _tournamentClient.DeleteTournamentRequest(token, tournamentId);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> EditAcceptedTournament(string token, TournamentRequestModel model)
        {
            var response = await _tournamentClient.EditAcceptedTournament(token, model);
            return response.StatusCode;
        }

        public async Task<ItemResponseModel<TournamentRequestModel>> ParticipateInTournament(string token, int tournamentId)
        {
            var response = await _tournamentClient.ParticipateInTournament(token, tournamentId);
            return await GetTournamentRequestFromResponse(response, tournamentId);
        }

        public async Task<ItemResponseModel<TournamentRequestModel>> GetTournamentRequest(string token, int tournamentId)
        {
            var response = await _tournamentClient.GetTournamentRequest(token, tournamentId);
            return await GetTournamentRequestFromResponse(response, tournamentId);
        }

        public async Task<HttpStatusCode> ParticipateInTournament(string token, TournamentRequestModel model)
        {
            var response = await _tournamentClient.ParticipateInTournament(token, model);
            return response.StatusCode;
        }

        private async Task<ItemsResponseModel<TournamentModel>> GetTournamentsFromResponse(HttpResponseMessage response)
        {
            List<TournamentModel> tournaments = new List<TournamentModel>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                tournaments = JsonConvert.DeserializeObject<List<TournamentModel>>(content);
            }

            return new ItemsResponseModel<TournamentModel>
            {
                StatusCode = response.StatusCode,
                Items = tournaments
            };
        }

        private async Task<ItemResponseModel<TournamentRequestModel>> GetTournamentRequestFromResponse(HttpResponseMessage response, int tournamentId)
        {
            TournamentRequestModel model = new TournamentRequestModel() { TournamentId = tournamentId, Students = new List<StudentFullModel>() };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<TournamentRequestModel>(content);
            }

            return new ItemResponseModel<TournamentRequestModel>
            {
                StatusCode = response.StatusCode,
                Item = model
            };
        }
    }
}
