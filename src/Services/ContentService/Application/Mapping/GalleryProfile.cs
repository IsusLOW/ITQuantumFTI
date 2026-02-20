using AutoMapper;
using ContentService.Application.DTOs;
using ContentService.Domain.Entities;

namespace ContentService.Application.Mapping
{
    public class GalleryProfile : Profile
    {
        public GalleryProfile()
        {
            CreateMap<GalleryImage, GalleryImageDto>();
        }
    }
}
