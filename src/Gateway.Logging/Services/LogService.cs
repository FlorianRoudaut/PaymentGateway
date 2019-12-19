using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Common.Commands;
using Gateway.Common.Events;
using Gateway.Logging.Model;
using Gateway.Logging.Repositories;

namespace Gateway.Logging.Services
{
    public class LogService : ILogService
    {
        private ILogRepository _logRepository { get; }

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task SaveAsync(CreateLog logEvent)
        {
            var log = new Log();
            log.Id = Guid.NewGuid();
            log.MerchantId = logEvent.MerchantId;
            log.PaymentId = logEvent.PaymentId;
            log.CreatedAt = logEvent.CreatedAt;
            log.LogLevel = logEvent.LogLevel;
            log.LogCode = logEvent.LogCode;
            log.LogMessage = logEvent.LogMessage;

            await _logRepository.AddAsync(log);
        }
    }
}
