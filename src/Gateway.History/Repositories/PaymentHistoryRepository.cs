using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Common.Model;
using Gateway.History.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Gateway.History.Repositories
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        public readonly IMongoDatabase _database;

        private IMongoCollection<PaymentHistory> Collection
                 => _database.GetCollection<PaymentHistory>("PaymentHistory");

        public PaymentHistoryRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(PaymentHistory history) => await Collection.InsertOneAsync(history);
    }
}
