using ContentService.Domain.Entities;

namespace ContentService.Application.Repositories
{
    public interface IGalleryRepository
    {
        Task<IEnumerable<GalleryImage>> GetAllAsync();
    }
}
