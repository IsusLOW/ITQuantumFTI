using CourseService.Domain.Entities;
using CourseService.Application.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseService.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private static readonly List<Course> _courses = new List<Course>
        {
            new Course { Id = 1, Name = "Основы C#", Description = "Изучение основ языка C#" },
            new Course { Id = 2, Name = "Продвинутый C#", Description = "Углубленное изучение C#" },
            new Course { Id = 3, Name = "ASP.NET Core", Description = "Создание веб-приложений с помощью ASP.NET Core" },
            new Course { Id = 4, Name = "Архитектура микросервисов", Description = "Проектирование и создание микросервисов" },
            new Course { Id = 5, Name = "Разработка на React", Description = "Создание современных UI с помощью React" },
            new Course { Id = 6, Name = "DevOps для начинающих", Description = "Введение в практики DevOps" },
            new Course { Id = 7, Name = "Docker и Kubernetes", Description = "Контейнеризация и оркестрация приложений" }
        };
        private static int _nextId = 8;

        public Task<IReadOnlyList<Course>> GetAllAsync()
        {
            return Task.FromResult<IReadOnlyList<Course>>(_courses);
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(course);
        }

        public Task AddAsync(Course entity)
        {
            entity.Id = _nextId++;
            _courses.Add(entity);
            return Task.FromResult(entity);
        }

        public Task UpdateAsync(Course entity)
        {
            var existingCourse = _courses.FirstOrDefault(c => c.Id == entity.Id);
            if (existingCourse != null)
            {
                existingCourse.Name = entity.Name;
                existingCourse.Description = entity.Description;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Course entity)
        {
            var courseToRemove = _courses.FirstOrDefault(c => c.Id == entity.Id);
            if (courseToRemove != null)
            {
                _courses.Remove(courseToRemove);
            }
            return Task.CompletedTask;
        }
    }
}
