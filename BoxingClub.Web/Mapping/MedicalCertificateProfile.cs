using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class MedicalCertificateProfile : Profile
    {
        public MedicalCertificateProfile()
        {
            CreateMap<MedicalCertificate, MedicalCertificateDTO>().ReverseMap();
            CreateMap<MedicalCertificateDTO, MedicalCertificateViewModel>().ReverseMap();
            CreateMap<MedicalCertificateViewModel, MedicalCertificateModel>().ReverseMap();
            CreateMap<MedicalCertificateDTO, MedicalCertificateModel>().ReverseMap();
        }
    }
}
