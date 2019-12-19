using Gateway.Authentication.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Authentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly IMongoDatabase _database;

        private IMongoCollection<User> Collection
                => _database.GetCollection<User>("Users");

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await Collection.
                    AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await Collection.
                    AsQueryable()
                    .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await Collection.
                    AsQueryable()
                    .FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task AddAsync(User merchant)
        {
            await Collection.InsertOneAsync(merchant);
        }
    }
}
