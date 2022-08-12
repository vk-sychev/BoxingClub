using AutoMapper;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentFullModel, StudentFullViewModel>().ReverseMap();

            CreateMap(typeof(PageViewModel<StudentLiteViewModel>), typeof(PageModel<StudentLiteModel>)).ReverseMap();
            CreateMap<StudentLiteViewModel, StudentLiteModel>().ReverseMap();
            CreateMap<StudentFullViewModel, StudentFullModel>().ReverseMap();
        }
    }
}
