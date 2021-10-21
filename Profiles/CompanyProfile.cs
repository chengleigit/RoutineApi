using AutoMapper;
using Routine.Api.DTO;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(dest=>dest.CompanyName,opt=>opt.MapFrom(src=>src.Name));
        }
    }
}
