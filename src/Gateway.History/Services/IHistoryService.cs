using Gateway.Common.Commands;
using Gateway.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.PaymentsProcessing.Services
{
    public interface IHistoryService
    {
        Task SaveAsync(PaymentProcessed @event);
        Task SaveAsync(PaymentFailed @event);
    }
}
