using ContentService.Application.DTOs;
using ContentService.Application.Repositories;

namespace ContentService.Application.Services
{
    public interface IGalleryAppService
    {
        Task<IReadOnlyList<GalleryImageDto>> GetAllAsync();
    }
}
