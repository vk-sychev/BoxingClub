using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using Microsoft.AspNetCore.Identity;

namespace BoxingClub.Web.Mapping
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            CreateMap<AccountResultDTO, IdentityResult>(MemberList.Source).ReverseMap();
            CreateMap<AccountErrorDTO, IdentityError>(MemberList.Source).ReverseMap();
            CreateMap<SignInResultDTO, SignInResult>(MemberList.Source).ReverseMap();
        }
    }
}
