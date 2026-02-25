using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseService.Domain.Entities;

namespace CourseService.Application.Repositories
{
    public interface ICourseRepository
    {
        Task<IReadOnlyList<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course entity);
        Task UpdateAsync(Course entity);
        Task DeleteAsync(Course entity);
    }
}