﻿using AutoMapper;
using Students.API.Models;
using Students.BLL.DomainEntities;
using Students.DAL.Entities;

namespace Students.API.Mapping
{
    public class BoxingGroupProfile : Profile
    {
        public BoxingGroupProfile()
        {
            CreateMap<BoxingGroup, BoxingGroupDTO>(MemberList.Source).ReverseMap();
        }
    }
}
