﻿using System.Linq;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentFullDTO, StudentFullViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, StudentLiteViewModel>().ReverseMap();
            
            CreateMap<StudentLiteDTO, StudentFullDTO>(MemberList.Source).ReverseMap();
            CreateMap<StudentFullModel, StudentFullViewModel>().ReverseMap();

            CreateMap(typeof(PageViewModel<StudentLiteViewModel>), typeof(PageModel<StudentLiteModel>)).ReverseMap();
            CreateMap<StudentLiteViewModel, StudentLiteModel>().ReverseMap();
            CreateMap<StudentFullViewModel, StudentFullModel>().ReverseMap();
            CreateMap<StudentFullModel, StudentFullDTO>().ReverseMap();
        }
    }
}
