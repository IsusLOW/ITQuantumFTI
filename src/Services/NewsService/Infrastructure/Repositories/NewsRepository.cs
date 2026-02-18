using NewsService.Application.Repositories;
using NewsService.Domain.Entities;

namespace NewsService.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        // In-memory data store for demonstration purposes.
        // A real application would use a database context.
        private static readonly List<News> _news = new List<News>
        {
            new News { Id = 1, Head = "Запуск нового курса по машинному обучению", Description = "Мы рады объявить о запуске нашего нового курса по машинному обучению, который начнется в следующем месяце.", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-1) },
            new News { Id = 2, Head = "Хакатон по разработке веб-приложений", Description = "Примите участие в нашем ежегодном хакатоне и выиграйте удивительные призы. Регистрация уже открыта!", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-2) },
            new News { Id = 3, Head = "День открытых дверей в IT-Квантум", Description = "Приглашаем всех желающих на день открытых дверей, где вы сможете познакомиться с нашими преподавателями и курсами.", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new News { Id = 4, Head = "Наши студенты заняли первое место на олимпиаде по программированию", Description = "Поздравляем наших талантливых студентов с победой!", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-5) },
            new News { Id = 5, Head = "Новый партнер: компания 'Future-Tech'", Description = "Мы заключили партнерское соглашение с ведущей IT-компанией 'Future-Tech'.", ImageUrls = new List<string> { "/images/news/future_tech_partnership.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-7) },
            new News { Id = 6, Head = "Летняя школа по кибербезопасности", Description = "Открыт набор в летнюю школу для старшеклассников, посвященную основам кибербезопасности.", ImageUrls = new List<string> { "/images/news/cybersecurity_school.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-10) },
            new News { Id = 7, Head = "Вебинар: 'Карьера в IT'", Description = "Присоединяйтесь к нашему бесплатному вебинару, чтобы узнать о карьерных возможностях в IT-сфере.", ImageUrls = new List<string> { "/images/news/career_webinar.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-12) },
            new News { Id = 8, Head = "Обновление учебных программ", Description = "Мы обновили наши учебные программы, добавив самые актуальные технологии и фреймворки.", ImageUrls = new List<string> { "/images/news/curriculum_update.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-15) },
            new News { Id = 9, Head = "Экскурсия в офис 'Innovate Solutions'", Description = "Наши студенты посетили офис компании 'Innovate Solutions' и познакомились с реальными проектами.", ImageUrls = new List<string> { "/images/news/office_tour.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-20) },
            new News { Id = 10, Head = "Мастер-класс по 3D-моделированию", Description = "Не пропустите наш эксклюзивный мастер-класс по созданию 3D-моделей в Blender.", ImageUrls = new List<string> { "/images/news/3d_workshop.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-25) },
            new News { Id = 11, Head = "Завершился курс по разработке игр", Description = "Поздравляем выпускников курса по разработке игр на Unity!", ImageUrls = new List<string> { "/images/news/gamedev_graduation.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-30) },
            new News { Id = 12, Head = "Мы в социальных сетях", Description = "Подписывайтесь на наши страницы в социальных сетях, чтобы быть в курсе всех новостей и анонсов.", ImageUrls = new List<string> { "/images/news/social_media.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-35) }
        };

        private static int _nextId = 13;

        public Task<IEnumerable<News>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<News>>(_news.OrderByDescending(n => n.CreatedAt));
        }

        public Task<IEnumerable<News>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var pagedNews = _news
                .OrderByDescending(n => n.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Task.FromResult<IEnumerable<News>>(pagedNews);
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
