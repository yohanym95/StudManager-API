using AutoMapper;
using StudManager.Data.Data.Entities;
using StudManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Mappers
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Course, CourseModel>()
                .ForMember( o => o.Id, opt => opt.MapFrom(src => 0))
                 .ReverseMap();

            CreateMap<Fees, FeesModel>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => 0))
                 .ReverseMap();
            CreateMap<Subject, SubjectModel>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => 0))
                 .ReverseMap();
        }
    }
}
