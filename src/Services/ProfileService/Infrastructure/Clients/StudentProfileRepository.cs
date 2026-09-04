using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Application.Repositories.Clients;
using ProfileService.Domain.Clients;

namespace ProfileService.Infrastructure.Clients
{
    public class StudentProfileRepository : IStudentProfileRepository
    {
        private static readonly List<ClientProfile> studentProfiles = [];
        private static int nextId = 0;

        public Task<IReadOnlyList<ClientProfile>> GetAllAsync()
        {
            return Task.FromResult<IReadOnlyList<ClientProfile>>(studentProfiles);
        }

        public Task<ClientProfile?> GetByIdAsync(int id)
        {
            var studentProfile = studentProfiles.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(studentProfile);
        }

        public Task AddAsync(ClientProfile entity)
        {
            entity.Id = nextId++;
            studentProfiles.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ClientProfile entity)
        {
            var existingStudent = studentProfiles.FirstOrDefault(p => p.Id == entity.Id);

            if(existingStudent != null)
            {
                existingStudent.LastName = entity.LastName;
                existingStudent.FirstName = entity.FirstName;
                existingStudent.SecondName = entity.SecondName;
                existingStudent.PhoneNumber = entity.PhoneNumber;
                existingStudent.Birthday = entity.Birthday;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(ClientProfile entity)
        {
            var studentProfile = studentProfiles.FirstOrDefault(p => p.Id == entity.Id);
            if(studentProfile != null)
            {
                studentProfiles.Remove(studentProfile);
            }
            return Task.CompletedTask;
        }
    }
}