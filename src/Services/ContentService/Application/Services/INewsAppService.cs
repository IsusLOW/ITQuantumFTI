using ContentService.Application.DTOs;

namespace ContentService.Application.Services
{
    public interface INewsAppService
    {
        Task<IReadOnlyList<NewsDto>> GetNewsAsync();
        Task<IReadOnlyList<NewsDto>> GetNewsWithPaginationAsync(int pageNumber, int pageSize);
        Task<NewsDto> CreateNewsAsync(CreateNewsDto dto);
        Task<NewsDto> UpdateNewsAsync(int id, UpdateNewsDto dto);
        Task DeleteNewsAsync(int id);
    }
}
