using AutoMapper;
using Helsi.DataAccess.Models;
using Helsi.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helsi.DomainLogic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();

            CreateMap<ContactType, ContactTypeDto>().ReverseMap();

            CreateMap<AdditionalContact, AdditionalContactDto>().ReverseMap();
            
            CreateMap<Gender, GenderDto>().ReverseMap();
        }
    }
}
