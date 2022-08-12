using HttpClientAdapters.Models;
using HttpClients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientAdapters.Interfaces
{
    public interface ITournamentClientAdapter
    {
        Task<ItemsResponseModel<TournamentModel>> GetTournaments(string token);

        Task<ItemResponseModel<TournamentModel>> GetTournament(string token, int id);

        Task<HttpStatusCode> CreateTournament(string token, TournamentModel model);

        Task<HttpStatusCode> EditTournament(string token, TournamentModel model);

        Task<HttpStatusCode> DeleteTournament(string token, int id);


        Task<ItemsResponseModel<TournamentModel>> GetAcceptedTournaments(string token);

        Task<ItemResponseModel<TournamentRequestModel>> ParticipateInTournament(string token, int id);

        Task<HttpStatusCode> ParticipateInTournament(string token, TournamentRequestModel model);

        Task<HttpStatusCode> EditAcceptedTournament(string token, TournamentRequestModel model);

        Task<HttpStatusCode> DeleteTournamentRequest(string token, int tournamentId);

        Task<ItemResponseModel<TournamentRequestModel>> GetTournamentRequest(string token, int tournamentId);
    }
}
