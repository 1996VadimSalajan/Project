using AutoMapper;
using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.DTO;

namespace WebApplication3.AutoMaper
{
    public class MaperProfile : Profile
    {
        public MaperProfile()
        {
            CreateMap<Cours, CoursDTO>();
            CreateMap<Address, AddressDTO>();
        }
    }
}