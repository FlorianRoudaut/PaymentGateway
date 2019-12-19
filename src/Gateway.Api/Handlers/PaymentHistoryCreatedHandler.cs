using Gateway.Api.Services;
using Gateway.Common.Commands;
using Gateway.Common.Events;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Api.Handlers
{
    public class PaymentHistoryCreatedHandler : IEventHandler<PaymentHistoryCreated>
    {
        private IBusClient _busClient;
        private IPaymentResultService _resultService;

        public PaymentHistoryCreatedHandler(IBusClient busClient, IPaymentResultService resultService)
        {
            _busClient = busClient;
            _resultService = resultService;
        }

        public async Task HandleAsync(PaymentHistoryCreated @event)
        {
            try
            {
                await _resultService.SaveAsync(@event);

                var logCreated = new CreateLog(@event.UserId, @event.PaymentId, LogLevel.Info, "Process_Payment_7",
                    "Saved Payment Result");
                await _busClient.PublishAsync(logCreated);
            }
            catch (Exception ex)
            {
                var logCreated = new CreateLog(@event.UserId, @event.PaymentId, LogLevel.Error, "Save_Payment_Failed",
                    "Cannot save Failed payment  because :" + ex.Message);
                await _busClient.PublishAsync(logCreated);
            }
        }

    }
}
