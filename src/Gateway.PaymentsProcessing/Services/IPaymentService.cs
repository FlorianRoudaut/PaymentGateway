using Gateway.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.PaymentsProcessing.Services
{
    public interface IPaymentService
    {
        Task<IAcquirerCommand> ProcessAsync(ProcessPayment command);

    }
}
