using AutoMapper;
using SliderService.Domain.Entities;
using SliderService.Application.DTOs;

namespace SliderService.Application.Mapping
{
    public class SliderProfile : Profile
    {
        public SliderProfile() 
        {
            CreateMap<Slider, SliderDto>();
            CreateMap<CreateSlideDto, Slider>();
            CreateMap<UpdateSlideDto, Slider>();
        }
    }
}