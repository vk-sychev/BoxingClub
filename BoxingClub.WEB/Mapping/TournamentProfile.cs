using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentDTO>(MemberList.Source).ForMember(dest => dest.AgeCategories, opt => opt.MapFrom(src => src.Categories.Select(x => x.AgeCategory)))
                                                                   .ForMember(dest => dest.WeightCategories, opt => opt.MapFrom(src => src.Categories.Select(x => x.WeightCategory)))
                                                                   .ForSourceMember(src => src.Categories, opt => opt.DoNotValidate())
                                                                   .ReverseMap()
                                                                   .ForMember(dest => dest.Categories, opt => opt.Ignore());

            CreateMap<TournamentDTO, TournamentViewModel>().ReverseMap();
        }
    }

    public class AgeCategoryResolver : IValueResolver<Tournament, TournamentDTO, List<AgeCategory>>
    {
        public List<AgeCategory> Resolve(Tournament source, TournamentDTO destination, List<AgeCategory> destMember, ResolutionContext context)
        {
            List<AgeCategory> ageCategories = new List<AgeCategory>();
            foreach(var item in source.Categories)
            {
                ageCategories.Add(item.AgeCategory);
            }
            return ageCategories;
        }
    }
}
