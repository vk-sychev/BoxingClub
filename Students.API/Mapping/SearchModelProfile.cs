using AutoMapper;
using HttpClients.Models;
using Students.API.Models;
using Students.BLL.DomainEntities;

namespace Students.API.Mapping
{
    public class SearchModelProfile : Profile
    {
        public SearchModelProfile()
        {
            CreateMap<SearchModelDTO, SearchModel>().ReverseMap();
            
        }
    }
}
