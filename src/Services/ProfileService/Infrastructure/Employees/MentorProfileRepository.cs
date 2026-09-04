using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Application.Repositories.Employees;
using ProfileService.Domain.Employees;

namespace ProfileService.Infrastructure.Employees
{
    public class MentorProfileRepository : IMentorProfileRepository
    {
        private static readonly List<MentorProfile> mentorProfiles =
        [
            new() { Id = 1, FirstName = "Иван", LastName = "Иванов", Description = "Эксперт по C#" },
            new() { Id = 2, FirstName = "Петр", LastName = "Петров", Description = "Знаток архитектуры" },
            new() { Id = 3, FirstName = "Анна", LastName = "Сидорова", Description = "Профессионал в Azure" },
            new() { Id = 4, FirstName = "Екатерина", LastName = "Кузнецова", Description = "Специалист по базам данных" },
            new() { Id = 5, FirstName = "Дмитрий", LastName = "Соколов", Description = "Мастер по фронтенду" },
            new() { Id = 6, FirstName = "Мария", LastName = "Лебедева", Description = "Гуру DevOps" }
        ];
        private static int _nextId = 7;

        public Task<IReadOnlyList<MentorProfile>> GetAllAsync()
        {
            return Task.FromResult<IReadOnlyList<MentorProfile>>(mentorProfiles);
        }

        public Task<MentorProfile?> GetByIdAsync(int id)
        {
            var mentor = mentorProfiles.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(mentor);
        }

        public Task AddAsync(MentorProfile entity)
        {
            entity.Id = _nextId++;
            mentorProfiles.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MentorProfile entity)
        {
            var existingMentor = mentorProfiles.FirstOrDefault(m => m.Id == entity.Id);
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

        public Task DeleteAsync(MentorProfile entity)
        {
            var mentorToRemove = mentorProfiles.FirstOrDefault(m => m.Id == entity.Id);
            if (mentorToRemove != null)
            {
                mentorProfiles.Remove(mentorToRemove);
            }
            return Task.CompletedTask;
        }
    }
}