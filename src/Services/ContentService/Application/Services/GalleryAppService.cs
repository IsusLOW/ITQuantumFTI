using AutoMapper;
using ContentService.Application.DTOs;
using ContentService.Application.Repositories;
using ContentService.Domain.Entities;

namespace ContentService.Application.Services
{
    public class GalleryAppService : IGalleryAppService
    {
        private readonly IGalleryRepository _repository;
        private readonly IMapper _mapper;

        public GalleryAppService(IGalleryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GalleryImageDto>> GetAllAsync()
        {
            var images = await _repository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<GalleryImageDto>>(images);
        }
    }
}
