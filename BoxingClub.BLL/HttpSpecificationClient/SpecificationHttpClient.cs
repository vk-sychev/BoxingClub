using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.HttpSpecificationClient.Models;
using BoxingClub.BLL.Interfaces;
using Newtonsoft.Json;

namespace BoxingClub.BLL.Implementation.HttpSpecificationClient
{
    public class SpecificationHttpClient : ISpecificationClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public SpecificationHttpClient(HttpClient httpClient,
                                       IMapper mapper)
        {
            _httpClient = httpClient; //проверка
            _mapper = mapper;
        }


        public async Task<TournamentSpecification> GetTournamentSpecifications(int tournamentId)
        {
            var url = $"{_httpClient.BaseAddress}{tournamentId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var specification = JsonConvert.DeserializeObject<SpecificationModelFromServer>(await response.Content.ReadAsStringAsync());

            var mappedSpecification = _mapper.Map<TournamentSpecification>(specification);

            return mappedSpecification;
        }
    }
}
