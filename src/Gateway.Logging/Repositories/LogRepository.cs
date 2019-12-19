using Gateway.Logging.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Logging.Repositories
{
    public class LogRepository : ILogRepository
    {
        public readonly IMongoDatabase _database;

        private IMongoCollection<Log> Collection
                 => _database.GetCollection<Log>("Log");

        public LogRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Log log) => await Collection.InsertOneAsync(log);
    }
}
