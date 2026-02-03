using AutoMapper;
using NewsService.Application.DTOs;
using NewsService.Domain.Entities;

namespace NewsService.Application.Mapping
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>();
            CreateMap<CreateNewsDto, News>();
            CreateMap<UpdateNewsDto, News>();
        }
    }
}