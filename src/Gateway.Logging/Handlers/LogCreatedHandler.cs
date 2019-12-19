using Gateway.Common.Commands;
using Gateway.Common.Events;
using Gateway.Logging.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Logging.Handlers
{
    public class LogCreatedHandler : ICommandHandler<CreateLog>
    {
        private IBusClient _busClient;
        private ILogService _logService;

        public LogCreatedHandler(IBusClient busClient, ILogService logService)
        {
            _busClient = busClient;
            _logService = logService;
        }

        public async Task HandleAsync(CreateLog @event)
        {
            try
            {
                await _logService.SaveAsync(@event);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot save Log " + @event.ToString() + " because :" + ex.Message);
            }
        }
    }
}
