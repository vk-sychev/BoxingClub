using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentFullDTO, StudentFullViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, StudentLiteViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, Student>(MemberList.Source).ReverseMap();
            CreateMap<StudentFullDTO, Student>().ReverseMap();
        }
    }
}
