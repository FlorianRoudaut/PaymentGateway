using Gateway.Logging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Logging.Repositories
{
    public interface ILogRepository
    {
        public Task AddAsync(Log log);
    }
}
