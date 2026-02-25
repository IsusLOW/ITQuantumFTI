using AutoMapper;
using MentorService.Application.DTOs;
using MentorService.Domain.Entities;

namespace MentorService.Application.Mapping
{
    public class MentorProfile : Profile
    {
        public MentorProfile()
        {
            // From Entity to DTO
            CreateMap<Mentor, MentorDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.GetFullName()));

            // From DTO to Entity
            CreateMap<CreateMentorDto, Mentor>();
            CreateMap<UpdateMentorDto, Mentor>();
        }
    }
}
