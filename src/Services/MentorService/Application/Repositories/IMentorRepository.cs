using MentorService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorService.Application.Repositories
{
    public interface IMentorRepository
    {
        Task<IReadOnlyList<Mentor>> GetAllAsync();
        Task<Mentor?> GetByIdAsync(int id);
        Task AddAsync(Mentor entity);
        Task UpdateAsync(Mentor entity);
        Task DeleteAsync(Mentor entity);
    }
}
