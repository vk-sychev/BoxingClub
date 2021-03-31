﻿using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentFullDTO, StudentFullViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, StudentLiteViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, Student>().ReverseMap();
            CreateMap<StudentFullDTO, Student>().ReverseMap();
            CreateMap<SignUpViewModel, IdentityUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.NickName)).ReverseMap();
            CreateMap<SignInViewModel, userDTO>();
        }
    }
}
