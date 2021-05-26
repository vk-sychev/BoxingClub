using System.Linq;
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
            CreateMap<StudentLiteDTO, Student>(MemberList.Source).ForSourceMember(src => src.Experienced, opt => opt.DoNotValidate())
                                                                 .ForSourceMember(src => src.IsMedicalCertificateValid, opt => opt.DoNotValidate())
                                                                 .ReverseMap()
                                                                 .ForMember(dest => dest.Experienced, opt => opt.Ignore());
            CreateMap<StudentFullDTO, Student>(MemberList.Destination).ForSourceMember(src => src.Experienced, opt => opt.DoNotValidate())
                                                                      .ForSourceMember(src => src.IsMedicalCertificateValid, opt => opt.DoNotValidate())
                                                                      .ReverseMap()
                                                                      .ForMember(dest => dest.LastMedicalCertificate, opt => opt.MapFrom(src => src.MedicalCertificates.OrderBy(x => x.DateOfIssue).LastOrDefault()));
            CreateMap<StudentLiteDTO, StudentFullDTO>(MemberList.Source).ReverseMap();
        }
    }
}
