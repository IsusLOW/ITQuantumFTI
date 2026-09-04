using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Application.DTOs.Employees.Mentors;
using ProfileService.Domain.Employees;

namespace ProfileService.Application.Mapping.Employees.Mentors
{
    public class MentorProfileMappingProfile : Profile
    {
        public MentorProfileMappingProfile()
        {
            CreateMap<MentorProfile, MentorProfileDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.GetFullName()));
            CreateMap<CreateMentorProfileDto, MentorProfile>();
            CreateMap<UpdateMentorProfileDto, MentorProfile>();
        }
    }
}