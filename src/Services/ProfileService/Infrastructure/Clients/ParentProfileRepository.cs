using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Application.Repositories.Clients;
using ProfileService.Domain.Clients;

namespace ProfileService.Infrastructure.Clients
{
    public class ParentProfileRepository : IParentProfileRepository
    {
        private static readonly List<ClientProfile> parentProfiles = [];
        private static int nextId = 0;

        public Task<IReadOnlyList<ClientProfile>> GetAllAsync()
        {
            return Task.FromResult<IReadOnlyList<ClientProfile>>(parentProfiles);
        }

        public Task<ClientProfile?> GetByIdAsync(int id)
        {
            var parentProfile = parentProfiles.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(parentProfile);
        }

        public Task AddAsync(ClientProfile entity)
        {
            entity.Id = nextId++;
            parentProfiles.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ClientProfile entity)
        {
            var existingParent = parentProfiles.FirstOrDefault(p => p.Id == entity.Id);

            if(existingParent != null)
            {
                existingParent.LastName = entity.LastName;
                existingParent.FirstName = entity.FirstName;
                existingParent.SecondName = entity.SecondName;
                existingParent.PhoneNumber = entity.PhoneNumber;
                existingParent.Birthday = entity.Birthday;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(ClientProfile entity)
        {
            var parentProfile = parentProfiles.FirstOrDefault(p => p.Id == entity.Id);
            if(parentProfile != null)
            {
                parentProfiles.Remove(parentProfile);
            }
            return Task.CompletedTask;
        }
    }
}