using AutoMapper;
using StudManager.Application.Commands.Authenticate;
using StudManager.Application.Commands.Courses;
using StudManager.Application.Commands.Fee;
using StudManager.Application.Responses.Fee;
using StudManager.Application.Responses.Students;
using StudManager.Application.Responses.Subjects;
using StudManager.Core.Entities;
using StudManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Mappers
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Course, CourseModel>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => 0))
                 .ReverseMap();

            CreateMap<Course, CourseCommand>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => 0))
                 .ReverseMap();

            CreateMap<Fees, CreateFessCommand>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => 0))
                 .ReverseMap();

            CreateMap<Fees, FeesResponse>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => 0))
                 .ReverseMap();

            CreateMap<ApplicationUser, StudentResponse>()
                 .ReverseMap();

            CreateMap<Subject, SubjectResponse>()
                 .ReverseMap();

            CreateMap<LoginCommand, LoginModel>()
                 .ReverseMap();

            //CreateMap<Subject, SubjectModel>()
            //    .ForMember(o => o.Id, opt => opt.MapFrom(src => 0))
            //     .ReverseMap();
        }
    }
}
