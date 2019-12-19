using Gateway.Common.Commands;
using Gateway.Common.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Common.Acquirers
{
    public interface IAcquirerService
    {
        public Task<PaymentProcessed> ProcessAsync(IAcquirerCommand processPayment);
    }
}
