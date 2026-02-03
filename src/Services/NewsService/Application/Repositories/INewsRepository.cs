using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsService.Domain.Entities;

namespace NewsService.Application.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News?> GetByIdAsync(int id);
        Task AddAsync(News news);
        Task UpdateAsync(News news);
        Task DeleteAsync(News news);       
    }
}