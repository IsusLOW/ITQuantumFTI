using ContentService.Application.DTOs;
using ContentService.Application.Repositories;
using ContentService.Domain.Entities;

namespace ContentService.Infrastructure.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private static readonly List<GalleryImage> _images = new List<GalleryImage>
        {
            new GalleryImage { Id = 1, ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", Title = "IT-Квантум главное фото", Order = 1 },
            new GalleryImage { Id = 2, ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", Title = "Учебный процесс", Order = 2 },
            new GalleryImage { Id = 3, ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", Title = "Наши студенты", Order = 3 },
            new GalleryImage { Id = 4, ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", Title = "Мероприятия", Order = 4 },
            new GalleryImage { Id = 5, ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", Title = "Лаборатория", Order = 5 },
            new GalleryImage { Id = 6, ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", Title = "Команда", Order = 6 }
        };

        public Task<IEnumerable<GalleryImage>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<GalleryImage>>(_images.OrderBy(g => g.Order));
        }
    }
}
