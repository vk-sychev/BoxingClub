using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;

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
