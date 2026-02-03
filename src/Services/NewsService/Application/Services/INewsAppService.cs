using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsService.Application.DTOs;

namespace NewsService.Application.Services
{
    public interface INewsAppService
    {
        Task<IReadOnlyList<NewsDto>> GetNewsAsync();
        Task<NewsDto> CreateNewsAsync(CreateNewsDto dto);
        Task<NewsDto> UpdateNewsAsync(int id, UpdateNewsDto dto);
        Task DeleteNewsAsync(int id);
    }
}