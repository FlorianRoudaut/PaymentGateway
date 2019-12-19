using Gateway.Common.Commands;
using Gateway.Common.Events;
using Gateway.Common.Model;
using Gateway.PaymentsProcessing.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.PaymentsProcessing.Handlers
{
    public class ProcessPaymentHandler : ICommandHandler<ProcessPayment>
    {
        private IBusClient _busClient;
        private IPaymentService _paymentService;

        public ProcessPaymentHandler(IBusClient busClient, IPaymentService paymentService)
        {
            _busClient = busClient;
            _paymentService = paymentService;
        }

        public async Task HandleAsync(ProcessPayment command)
        {
            try
            {
                var acquirerCommand = await _paymentService.ProcessAsync(command);

                await _busClient.PublishAsync(acquirerCommand);

                var logCreated = new CreateLog(command.UserId, command.PaymentId, LogLevel.Info, "Process_Payment_2", 
                    "Sent Process Payment Request to "+acquirerCommand.AquirerName+" Service");
                await _busClient.PublishAsync(logCreated);

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
