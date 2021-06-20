using AutoMapper;
using HttpClients.Models.SpecModels;
using Tournaments.BLL.Entities;
using Tournaments.BLL.Entities.SpecificationModels;

namespace Tournaments.API.Mapping
{
    public class AgeGroupProfile : Profile
    {
        public AgeGroupProfile()
        {
            CreateMap<AgeGroupModel, AgeGroupDTO>(MemberList.Destination).ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Sex));
            CreateMap<AgeGroupDTO, AgeGroup>().ReverseMap();
        }
    }
}
