using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthService.Domain.Entities;

namespace AuthService.Application.Repositories
{
    public interface IAuthRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> FindByAsync(Expression<Func<User, bool>> predicate);
    }
}