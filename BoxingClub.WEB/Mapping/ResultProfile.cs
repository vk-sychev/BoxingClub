using AutoMapper;
using BoxingClub.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
