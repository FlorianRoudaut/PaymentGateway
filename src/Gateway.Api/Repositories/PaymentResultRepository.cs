using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Api.Domain;
using Gateway.Common.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Gateway.Api.Repositories
{
    public class PaymentResultRepository : IPaymentResultRepository
    {
        public readonly IMongoDatabase _database;

        private IMongoCollection<PaymentResult> Collection
                 => _database.GetCollection<PaymentResult>("PaymentResult");

        public PaymentResultRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<PaymentResult> GetAsync(Guid id)
        {
            return await Collection.
                    AsQueryable().
                    FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<PaymentResult>> BrowseAsync(Guid merchantId)
        {
            return await Collection.
                AsQueryable()
                .Where(t => t.MerchantId == merchantId).ToListAsync();
        }

        public async Task AddAsync(PaymentResult history) => await Collection.InsertOneAsync(history);
    }
}
