using AutoMapper;
using IdentityServer.BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Mapping
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            CreateMap<AccountResultDTO, IdentityResult>(MemberList.Source).ReverseMap();
            CreateMap<AccountErrorDTO, IdentityError>(MemberList.Source).ReverseMap();
        }
    }
}
