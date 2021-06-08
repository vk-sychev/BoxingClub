using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.DomainEntities.Models;
using BoxingClub.BLL.Interfaces.HttpSpecificationClient;
using Newtonsoft.Json;

namespace BoxingClub.BLL.Implementation.HttpSpecificationClient
{
    public class SpecificationHttpClient : ISpecificationHttpClient
    {
        private readonly HttpClient _httpClient;

        public SpecificationHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "httpClient is null");
        }


        public async Task<TournamentSpecificationModel> GetTournamentSpecifications(int tournamentId)
        {
            var url = $"{_httpClient.BaseAddress}{tournamentId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var specification = JsonConvert.DeserializeObject<TournamentSpecificationModel>(content);
            return specification;
        }
    }
}
