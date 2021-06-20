using HttpClients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Interfaces
{
    public interface ITournamentClient
    {
        Task<HttpResponseMessage> GetTournaments(string token);

        Task<HttpResponseMessage> GetTournament(string token, int id);

        Task<HttpResponseMessage> CreateTournament(string token, TournamentModel model);

        Task<HttpResponseMessage> EditTournament(string token, TournamentModel model);

        Task<HttpResponseMessage> DeleteTournament(string token, int id);


        Task<HttpResponseMessage> GetAcceptedTournaments(string token);

        Task<HttpResponseMessage> ParticipateInTournament(string token, int id);

        Task<HttpResponseMessage> ParticipateInTournament(string token, TournamentRequestModel model);

        Task<HttpResponseMessage> EditAcceptedTournament(string token, TournamentRequestModel model);

        Task<HttpResponseMessage> DeleteTournamentRequest(string token, int tournamentId);

        Task<HttpResponseMessage> GetTournamentRequest(string token, int tournamentId);
    }
}
