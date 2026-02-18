using MentorService.Domain.Entities;
using MentorService.Application.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorService.Infrastructure.Repositories
{
    public class MentorRepository : IMentorRepository
    {
        private static readonly List<Mentor> _mentors = new List<Mentor>
        {
            new Mentor { Id = 1, FirstName = "Иван", LastName = "Иванов", Description = "Эксперт по C#" },
            new Mentor { Id = 2, FirstName = "Петр", LastName = "Петров", Description = "Знаток архитектуры" },
            new Mentor { Id = 3, FirstName = "Анна", LastName = "Сидорова", Description = "Профессионал в Azure" },
            new Mentor { Id = 4, FirstName = "Екатерина", LastName = "Кузнецова", Description = "Специалист по базам данных" },
            new Mentor { Id = 5, FirstName = "Дмитрий", LastName = "Соколов", Description = "Мастер по фронтенду" },
            new Mentor { Id = 6, FirstName = "Мария", LastName = "Лебедева", Description = "Гуру DevOps" }
        };
        private static int _nextId = 7;

        public Task<IReadOnlyList<Mentor>> GetAllAsync()
        {
            return Task.FromResult<IReadOnlyList<Mentor>>(_mentors);
        }

        public Task<Mentor?> GetByIdAsync(int id)
        {
            var mentor = _mentors.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(mentor);
        }

        public Task AddAsync(Mentor entity)
        {
            entity.Id = _nextId++;
            _mentors.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Mentor entity)
        {
            var existingMentor = _mentors.FirstOrDefault(m => m.Id == entity.Id);
            if (existingMentor != null)
            {
                existingMentor.FirstName = entity.FirstName;
                existingMentor.SecondName = entity.SecondName;
                existingMentor.LastName = entity.LastName;
                existingMentor.Avatar = entity.Avatar;
                existingMentor.Description = entity.Description;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Mentor entity)
        {
            var mentorToRemove = _mentors.FirstOrDefault(m => m.Id == entity.Id);
            if (mentorToRemove != null)
            {
                _mentors.Remove(mentorToRemove);
            }
            return Task.CompletedTask;
        }
    }
}
