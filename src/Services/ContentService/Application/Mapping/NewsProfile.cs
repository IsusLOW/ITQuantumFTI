using AutoMapper;
using ContentService.Application.DTOs;
using ContentService.Domain.Entities;

namespace ContentService.Application.Mapping
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>();
            CreateMap<CreateNewsDto, News>();
            CreateMap<UpdateNewsDto, News>();
            
            // Для Update - пропускаем null значения
            CreateMap<UpdateNewsDto, News>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
