using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthService.Application.Repositories;
using AuthService.Domain.Entities;

namespace AuthService.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private static readonly List<User> _users = new();

        public Task<User> CreateAsync(User user)
        {
            user.Id = Guid.NewGuid();
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> FindByAsync(Expression<Func<User, bool>> predicate)
        {
            var user = _users.FirstOrDefault(predicate.Compile());
            return Task.FromResult(user);
        }
    }
}