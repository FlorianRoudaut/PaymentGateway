using Gateway.Authentication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Authentication.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);

        Task<User> GetByNameAsync(string name);

        Task<User> GetByLoginAsync(string login);

        Task AddAsync(User merchant);
    }
}
