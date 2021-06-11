﻿using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
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