using AutoMapper;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class MedicalCertificateProfile : Profile
    {
        public MedicalCertificateProfile()
        {
            CreateMap<MedicalCertificateViewModel, MedicalCertificateModel>().ReverseMap();
        }
    }
}
