using Gateway.Common.Commands;
using Gateway.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Logging.Services
{
    public interface ILogService
    {
        Task SaveAsync(CreateLog log);
    }
}
