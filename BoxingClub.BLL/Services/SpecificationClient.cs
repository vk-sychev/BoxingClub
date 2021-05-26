using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Enums;

namespace BoxingClub.BLL.Implementation.Services
{
    public class SpecificationClient : ISpecificationClient
    {
        private static readonly AgeCategoryDTO _adultAgeCategory = new AgeCategoryDTO()
        {
            StartAge = 19,
            EndAge = 40
        };

        private static readonly AgeCategoryDTO _juniorAgeCategory = new AgeCategoryDTO()
        {
            StartAge = 17,
            EndAge = 18
        };

        private static readonly WeightCategoryDTO _weightCategoryAdultJuniorMales1 = new WeightCategoryDTO()
        {
            StartWeight = 52,
            EndWeight = 56,
        };

        private static readonly WeightCategoryDTO _weightCategoryAdultJuniorMales2 = new WeightCategoryDTO()
        {
            StartWeight = 56,
            EndWeight = 60,
        };

        private static readonly WeightCategoryDTO _weightCategoryAdultJuniorMales3 = new WeightCategoryDTO()
        {
            StartWeight = 69,
            EndWeight = 75,
        };

        private static readonly WeightCategoryDTO _weightCategoryAdultJuniorFemales1 = new WeightCategoryDTO()
        {
            StartWeight = 51,
            EndWeight = 54
        };

        private static readonly WeightCategoryDTO _weightCategoryAdultJuniorFemales2 = new WeightCategoryDTO()
        {
            StartWeight = 54,
            EndWeight = 57
        };

        private static readonly WeightCategoryDTO _weightCategoryAdultJuniorFemales3 = new WeightCategoryDTO()
        {
            StartWeight = 57,
            EndWeight = 60
        };

        private static readonly List<WeightCategoryDTO> _weightCategoriesAdultJuniorMales = 
            new List<WeightCategoryDTO>()
            {_weightCategoryAdultJuniorMales1, _weightCategoryAdultJuniorMales2, _weightCategoryAdultJuniorMales3};

        private static readonly List<WeightCategoryDTO> _weightCategoriesAdultJuniorFemales =
            new List<WeightCategoryDTO>()
            {_weightCategoryAdultJuniorFemales1, _weightCategoryAdultJuniorFemales2, _weightCategoryAdultJuniorFemales3};

        private static readonly AgeGroup _adultMales = new AgeGroup()
        {
            AgeCategory = _adultAgeCategory,
            WeightCategories = _weightCategoriesAdultJuniorMales,
            Gender = Gender.Male
        };

        private static readonly AgeGroup _adultFemales = new AgeGroup()
        {
            AgeCategory = _adultAgeCategory,
            WeightCategories = _weightCategoriesAdultJuniorFemales,
            Gender = Gender.Female
        };

        private static readonly AgeGroup _juniorFemales = new AgeGroup()
        {
            AgeCategory = _juniorAgeCategory,
            WeightCategories = _weightCategoriesAdultJuniorFemales,
            Gender = Gender.Female
        };

        private static readonly List<AgeGroup> _specifications = new List<AgeGroup>()
            {_adultMales, _adultFemales, _juniorFemales};

        public Task<TournamentSpecification> GetTournamentSpecifications(int tournamentId)
        {
            return Task.FromResult(new TournamentSpecification()
            {
                TournamentId = tournamentId,
                AgeGroups = _specifications
            });
        }
    }
}
