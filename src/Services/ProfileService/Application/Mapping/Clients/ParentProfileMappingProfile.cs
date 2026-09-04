using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Application.DTOs.Clients;
using ProfileService.Domain.Clients;

namespace ProfileService.Application.Mapping.Clients
{
    public class ParentProfileMappingProfile : Profile
    {
        public ParentProfileMappingProfile()
        {
            CreateMap<ClientProfile, ParentProfileDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.GetFullName()));

            CreateMap<CreateParentProfileDto, ClientProfile>();
            CreateMap<UpdateParentProfileDto, ClientProfile>();
        }
    }
}