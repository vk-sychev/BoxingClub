using AutoMapper;
using Students.API.Models;
using Students.BLL.DomainEntities;
using Students.DAL.Entities;

namespace Students.API.Mapping
{
    public class MedicalCertificateProfile : Profile
    {
        public MedicalCertificateProfile()
        {
            CreateMap<MedicalCertificate, MedicalCertificateDTO>().ReverseMap();
            CreateMap<MedicalCertificateDTO, MedicalCertificateViewModel>().ReverseMap();
        }
    }
}
