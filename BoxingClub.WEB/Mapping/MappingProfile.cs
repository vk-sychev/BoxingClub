using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;
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
            CreateMap<StudentFullDTO, CreateStudentViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, StudentViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, Student>().ReverseMap();
            CreateMap<StudentFullDTO, Student>().ReverseMap();
        }
    }
}
