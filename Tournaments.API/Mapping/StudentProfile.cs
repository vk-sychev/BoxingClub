using System.Linq;
using AutoMapper;
using HttpClients.Models;
using Tournaments.BLL.Entities;

namespace Tournaments.API.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentFullModel, StudentFullDTO>().ReverseMap();
        }
    }
}
