using System;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.DomainEntities.Models;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.HttpSpecificationClient;
using Microsoft.Extensions.Logging;

namespace BoxingClub.BLL.Implementation.HttpSpecificationClient
{
    public class SpecificationHttpClientAdapter : ISpecificationClient
    {
        private readonly ISpecificationHttpClient _specificationHttpClient;
        private readonly ILogger<SpecificationHttpClient> _logger;
        private readonly IMapper _mapper;

        public SpecificationHttpClientAdapter(ISpecificationHttpClient specificationHttpClient,
                                              ILogger<SpecificationHttpClient> logger,
                                              IMapper mapper)
        {
            _specificationHttpClient = specificationHttpClient;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TournamentSpecification> GetTournamentSpecifications(int tournamentId)
        {
            var specification = new TournamentSpecificationModel();
            var mappedSpecification = new TournamentSpecification();

            try
            {
                specification = await _specificationHttpClient.GetTournamentSpecifications(tournamentId);
                mappedSpecification = _mapper.Map<TournamentSpecification>(specification);
            }
            catch
            {
                _logger.LogError("Error occured while attempting to connect to spec server");
                return null;
            }

            return mappedSpecification;
        }
    }
}
