using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Domain.Employees;

namespace ProfileService.Application.Repositories.Employees
{
    public interface IMentorProfileRepository
    {
        Task<IReadOnlyList<MentorProfile>> GetAllAsync();
        Task<MentorProfile?> GetByIdAsync(int id);
        Task AddAsync(MentorProfile entity);
        Task UpdateAsync(MentorProfile entity);
        Task DeleteAsync(MentorProfile entity);
    }
}