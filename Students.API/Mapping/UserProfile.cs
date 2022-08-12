using AutoMapper;
using HttpClients.Models;
using Students.API.Models;
using Students.BLL.DomainEntities;

namespace Students.API.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();
        }
    }
}
