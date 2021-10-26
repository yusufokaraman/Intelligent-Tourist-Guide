﻿using AutoMapper;
using ITG.Entities.Concrete;
using ITG.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Services.AutoMapper.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityAddDto, City>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<CityUpdateDto, City>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
