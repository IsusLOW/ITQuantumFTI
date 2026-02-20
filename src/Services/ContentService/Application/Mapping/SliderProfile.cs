using AutoMapper;
using ContentService.Application.DTOs;
using ContentService.Domain.Entities;

namespace ContentService.Application.Mapping
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
