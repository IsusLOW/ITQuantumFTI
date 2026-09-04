using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Domain.Clients;

namespace ProfileService.Application.Repositories.Clients.Shared
{
    public interface ICleintProfileRepository
    {
        Task<IReadOnlyList<ClientProfile>> GetAllAsync();
        Task<ClientProfile?> GetByIdAsync(int id);
        Task AddAsync(ClientProfile entity);
        Task UpdateAsync(ClientProfile entity);
        Task DeleteAsync(ClientProfile entity);
    }
}