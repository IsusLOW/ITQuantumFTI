using ContentService.Application.Repositories;
using ContentService.Domain.Entities;

namespace ContentService.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private static readonly List<News> _news = new List<News>
        {
            new News { Id = 1, Head = "Запуск нового курса по машинному обучению", Description = "Мы рады объявить о запуске нашего нового курса по машинному обучению, который начнется в следующем месяце.", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-1), Author = "Иван Петров" },
            new News { Id = 2, Head = "Хакатон по разработке веб-приложений", Description = "Примите участие в нашем ежегодном хакатоне и выиграйте удивительные призы. Регистрация уже открыта!", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-2), Author = "Анна Сидорова" },
            new News { Id = 3, Head = "День открытых дверей в IT-Квантум", Description = "Приглашаем всех желающих на день открытых дверей, где вы сможете познакомиться с нашими преподавателями и курсами.", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-3), Author = "IT-Квантум" },
            new News { Id = 4, Head = "Наши студенты заняли первое место на олимпиаде по программированию", Description = "Поздравляем наших талантливых студентов с победой!", ImageUrls = new List<string> { "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80" }, CreatedAt = DateTime.UtcNow.AddDays(-5), Author = "Мария Иванова" },
            new News { Id = 5, Head = "Новый партнер: компания 'Future-Tech'", Description = "Мы заключили партнерское соглашение с ведущей IT-компанией 'Future-Tech'.", ImageUrls = new List<string> { "/images/news/future_tech_partnership.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-7), Author = "IT-Квантум" },
            new News { Id = 6, Head = "Летняя школа по кибербезопасности", Description = "Открыт набор в летнюю школу для старшеклассников, посвященную основам кибербезопасности.", ImageUrls = new List<string> { "/images/news/cybersecurity_school.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-10), Author = "Алексей Смирнов" },
            new News { Id = 7, Head = "Вебинар: 'Карьера в IT'", Description = "Присоединяйтесь к нашему бесплатному вебинару, чтобы узнать о карьерных возможностях в IT-сфере.", ImageUrls = new List<string> { "/images/news/career_webinar.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-12), Author = "Елена Кузнецова" },
            new News { Id = 8, Head = "Обновление учебных программ", Description = "Мы обновили наши учебные программы, добавив самые актуальные технологии и фреймворки.", ImageUrls = new List<string> { "/images/news/curriculum_update.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-15), Author = "IT-Квантум" },
            new News { Id = 9, Head = "Экскурсия в офис 'Innovate Solutions'", Description = "Наши студенты посетили офис компании 'Innovate Solutions' и познакомились с реальными проектами.", ImageUrls = new List<string> { "/images/news/office_tour.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-20), Author = "Дмитрий Попов" },
            new News { Id = 10, Head = "Мастер-класс по 3D-моделированию", Description = "Не пропустите наш эксклюзивный мастер-класс по созданию 3D-моделей в Blender.", ImageUrls = new List<string> { "/images/news/3d_workshop.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-25), Author = "Сергей Морозов" },
            new News { Id = 11, Head = "Завершился курс по разработке игр", Description = "Поздравляем выпускников курса по разработке игр на Unity!", ImageUrls = new List<string> { "/images/news/gamedev_graduation.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-30), Author = "IT-Квантум" },
            new News { Id = 12, Head = "Мы в социальных сетях", Description = "Подписывайтесь на наши страницы в социальных сетях, чтобы быть в курсе всех новостей и анонсов.", ImageUrls = new List<string> { "/images/news/social_media.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-35), Author = "Пресс-служба" },
            
            // Дополнительные новости для тестирования пагинации
            new News { Id = 13, Head = "Открыт набор на курс по Python-разработке", Description = "Приглашаем всех желающих на наш новый курс по Python-разработке. Курс рассчитан на 3 месяца и включает в себя изучение основ языка, работу с базами данных и создание веб-приложений.", ImageUrls = new List<string> { "/images/news/python_course.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-40), Author = "Ольга Николаева" },
            new News { Id = 14, Head = "Студенты IT-Квантум победили в региональном конкурсе", Description = "Поздравляем команду наших студентов с победой в региональном конкурсе IT-проектов! Их проект умного дома занял первое место среди 50 участников.", ImageUrls = new List<string> { "/images/news/contest_win.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-45), Author = "Иван Петров" },
            new News { Id = 15, Head = "Новое оборудование в лаборатории робототехники", Description = "В нашу лабораторию робототехники поступило новое современное оборудование для обучения и исследований.", ImageUrls = new List<string> { "/images/news/robot_lab.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-50), Author = "IT-Квантум" },
            new News { Id = 16, Head = "Мастер-класс по мобильной разработке", Description = "Состоится открытый мастер-класс по разработке мобильных приложений на Flutter. Приглашаются все желающие!", ImageUrls = new List<string> { "/images/news/flutter_workshop.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-55), Author = "Анна Сидорова" },
            new News { Id = 17, Head = "IT-Квантум на выставке 'Образование 2026'", Description = "Наш центр представил свои лучшие проекты на ежегодной выставке образования и технологий.", ImageUrls = new List<string> { "/images/news/education_expo.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-60), Author = "Пресс-служба" },
            new News { Id = 18, Head = "Запуск курса по Data Science", Description = "Открыт набор на продвинутый курс по анализу данных и машинному обучению. Начало занятий в следующем месяце.", ImageUrls = new List<string> { "/images/news/data_science.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-65), Author = "Мария Иванова" },
            new News { Id = 19, Head = "День программирования в IT-Квантум", Description = "Приглашаем на ежегодный день программирования! Соревнования, мастер-классы и призы от партнеров.", ImageUrls = new List<string> { "/images/news/coding_day.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-70), Author = "IT-Квантум" },
            new News { Id = 20, Head = "Интервью с выпускником: путь в Big Tech", Description = "Наш выпускник рассказывает о работе в крупной международной IT-компании и даёт советы начинающим.", ImageUrls = new List<string> { "/images/news/alumni_interview.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-75), Author = "Елена Кузнецова" },
            new News { Id = 21, Head = "Новый партнер: студия цифрового дизайна", Description = "Заключено партнерство с ведущей студией цифрового дизайна для проведения совместных мастер-классов.", ImageUrls = new List<string> { "/images/news/design_partner.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-80), Author = "IT-Квантум" },
            new News { Id = 22, Head = "Хакатон по IoT-решениям", Description = "Объявлен набор участников на хакатон по интернету вещей. Призовой фонд 100 000 рублей!", ImageUrls = new List<string> { "/images/news/iot_hackathon.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-85), Author = "Алексей Смирнов" },
            new News { Id = 23, Head = "Открытый урок по кибербезопасности", Description = "Приглашаем школьников на открытый урок по основам кибербезопасности и защите информации.", ImageUrls = new List<string> { "/images/news/cyber_lesson.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-90), Author = "Дмитрий Попов" },
            new News { Id = 24, Head = "IT-Квантум расширяется: новый корпус", Description = "С радостью сообщаем об открытии нового учебного корпуса с современными аудиториями и лабораториями.", ImageUrls = new List<string> { "/images/news/new_building.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-95), Author = "Пресс-служба" },
            new News { Id = 25, Head = "Курс по блокчейн-технологиям", Description = "Уникальная возможность изучить технологию блокчейн и создание смарт-контрактов на практических примерах.", ImageUrls = new List<string> { "/images/news/blockchain_course.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-100), Author = "Сергей Морозов" },
            new News { Id = 26, Head = "Встреча с представителями IT-индустрии", Description = "Состоится панельная дискуссия с ведущими специалистами IT-отрасли о тенденциях и перспективах рынка.", ImageUrls = new List<string> { "/images/news/industry_meetup.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-105), Author = "Ольга Николаева" },
            new News { Id = 27, Head = "Летняя практика для студентов", Description = "Открыт набор на летнюю производственную практику в компании-партнеры IT-Квантум.", ImageUrls = new List<string> { "/images/news/summer_internship.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-110), Author = "IT-Квантум" },
            new News { Id = 28, Head = "Победа в международном конкурсе", Description = "Наша команда заняла призовое место в международном конкурсе студенческих IT-проектов!", ImageUrls = new List<string> { "/images/news/international_win.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-115), Author = "Иван Петров" },
            new News { Id = 29, Head = "Новый курс по облачным технологиям", Description = "Изучите платформы AWS, Azure и Google Cloud на новом практическом курсе.", ImageUrls = new List<string> { "/images/news/cloud_course.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-120), Author = "Анна Сидорова" },
            new News { Id = 30, Head = "IT-Квантум: итоги года", Description = "Подводим итоги года: достижения, проекты, планы на будущее. Благодарим всех за участие!", ImageUrls = new List<string> { "/images/news/year_summary.webp" }, CreatedAt = DateTime.UtcNow.AddDays(-125), Author = "Пресс-служба" }
        };

        private static int _nextId = 31;

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
