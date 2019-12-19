using Gateway.Common.Mongo;
using Gateway.PaymentsProcessing.Domain;
using Gateway.PaymentsProcessing.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.PaymentsProcessing.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        IMongoDatabase _database;
        IMerchantRepository _merchantRepository;

        public CustomMongoSeeder(IMongoDatabase database, IMerchantRepository merchantRepository) : base(database)
        {
            _database = database;
            _merchantRepository = merchantRepository;
        }


        protected override async Task CustomSeedAsync()
        {
            var merchant1 = new Merchant("Amazon", "PiggyBank");
            var merchant2 = new Merchant("Netflix", "PiggyBank");
            var merchant3 = new Merchant("Apple", "OtherAquirer");

            var merchants = new List<Merchant>
            {
                merchant1,
                merchant2,
                merchant3
            };

            await Task.WhenAll(merchants.Select(x => _merchantRepository
                        .AddAsync(x)));
        }
    }
}
