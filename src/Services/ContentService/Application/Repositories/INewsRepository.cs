using ContentService.Domain.Entities;

namespace ContentService.Application.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<IEnumerable<News>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<News?> GetByIdAsync(int id);
        Task AddAsync(News news);
        Task UpdateAsync(News news);
        Task DeleteAsync(News news);
    }
}
