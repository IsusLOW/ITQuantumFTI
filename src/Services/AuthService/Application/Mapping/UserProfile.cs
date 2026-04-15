using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Responses;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Entity to DTO
            CreateMap<User, UserDto>();

            // Request to entity
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}