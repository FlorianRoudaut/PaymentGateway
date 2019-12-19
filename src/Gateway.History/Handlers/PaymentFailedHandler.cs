using Gateway.Common.Commands;
using Gateway.Common.Events;
using Gateway.Common.Model;
using Gateway.PaymentsProcessing.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.History.Handlers
{
    public class PaymentFailedHandler : IEventHandler<PaymentFailed>
    {
        private IBusClient _busClient;
        private IHistoryService _historyService;

        public PaymentFailedHandler(IBusClient busClient, IHistoryService historyService)
        {
            _busClient = busClient;
            _historyService = historyService;
        }

        public async Task HandleAsync(PaymentFailed @event)
        {
            try
            {
                await _historyService.SaveAsync(@event);

                var logCreated = new CreateLog(@event.UserId, @event.PaymentId, LogLevel.Info, "Process_Payment_6",
                        "Saved Failed Payment");
                await _busClient.PublishAsync(logCreated);

                var historyCreatedEvent = new PaymentHistoryCreated();
                @event.CopyPayment(historyCreatedEvent);
                historyCreatedEvent.Processed = false;
                await _busClient.PublishAsync(historyCreatedEvent);
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
