using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsService.Application.Repositories;
using NewsService.Domain.Entities;

namespace NewsService.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        // In-memory data store
        private static readonly List<News> _news = new List<News>
        {
            new News { Id = 1, Head = "First News", Description = "Description of the first news.", ImageUrls = new List<string> { "http://example.com/image1.jpg" } },
            new News { Id = 2, Head = "Second News", Description = "Description of the second news.", ImageUrls = new List<string> { "http://example.com/image2.jpg" } }
        };

        private static int _nextId = 3;

        public Task<IEnumerable<News>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<News>>(_news);
        }

        public Task<News?> GetByIdAsync(int id)
        {
            var newsItem = _news.FirstOrDefault(n => n.Id == id);
            return Task.FromResult(newsItem);
        }

        public Task AddAsync(News news)
        {
            news.Id = _nextId++;
            news.CreatedAt = DateTime.UtcNow;
            _news.Add(news);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(News news)
        {
            var existingNews = _news.FirstOrDefault(n => n.Id == news.Id);
            if (existingNews != null)
            {
                existingNews.Head = news.Head;
                existingNews.Description = news.Description;
                existingNews.ImageUrls = news.ImageUrls;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(News news)
        {
            var newsToRemove = _news.FirstOrDefault(n => n.Id == news.Id);
            if (newsToRemove != null)
            {
                _news.Remove(newsToRemove);
            }
            return Task.CompletedTask;
        }
    }
}
