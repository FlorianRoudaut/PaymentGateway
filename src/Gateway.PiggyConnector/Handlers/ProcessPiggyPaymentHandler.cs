using Gateay.PiggyConnector.Service;
using Gateway.Common.Acquirers;
using Gateway.Common.Commands;
using Gateway.Common.Events;
using Gateway.Common.Model;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateay.PiggyConnector.Handlers
{
    public class ProcessPiggyPaymentHandler: ICommandHandler<ProcessPiggyPayment>
    {
        private IBusClient _busClient;
        private IAcquirerService _piggyService;

        public ProcessPiggyPaymentHandler(IBusClient busClient, IAcquirerService piggyService)
        {
            _busClient = busClient;
            _piggyService = piggyService;
        }

        public async Task HandleAsync(ProcessPiggyPayment command)
        {
            try
            {
                var logCreated = new CreateLog(command.UserId, command.PaymentId, LogLevel.Info, "Process_Payment_3",
                        "Sent Process Payment Request to Piggy Bank API");
                await _busClient.PublishAsync(logCreated);

                var paymentProcessed = await _piggyService.ProcessAsync(command);

                await _busClient.PublishAsync(paymentProcessed);

                var logCreated2 = new CreateLog(command.UserId, command.PaymentId, LogLevel.Info, "Process_Payment_4",
                        "Payment "+paymentProcessed.Status+" by PiggyBank");
                await _busClient.PublishAsync(logCreated2);

                return;
            }
            catch (Exception ex)
            {
                var failed = new PaymentFailed("error", ex.Message);
                command.CopyPayment(failed);
                await _busClient.PublishAsync(failed);

                var logCreated = new CreateLog(command.UserId, command.PaymentId, LogLevel.Error, "Process_Payment_Failed",
                        "Process Payment Failed ");
                await _busClient.PublishAsync(logCreated);
            }
        }
    }
}
