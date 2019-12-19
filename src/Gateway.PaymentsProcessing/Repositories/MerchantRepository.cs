using Gateway.PaymentsProcessing.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.PaymentsProcessing.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        public readonly IMongoDatabase _database;

        private IMongoCollection<Merchant> Collection
                => _database.GetCollection<Merchant>("Merchants");

        public MerchantRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Merchant> GetByIdAsync(Guid id)
        {
            return await Collection.
                    AsQueryable()
                    .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Merchant> GetByNameAsync(string name)
        {
            return await Collection.
                    AsQueryable()
                    .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task AddAsync(Merchant merchant)
        {
            await Collection.InsertOneAsync(merchant);
        }
    }
}
