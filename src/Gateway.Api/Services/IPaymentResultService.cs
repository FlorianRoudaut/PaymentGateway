using Gateway.Api.Domain;
using Gateway.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Api.Services
{
    public interface IPaymentResultService
    {
        Task SaveAsync(PaymentHistoryCreated @event);

        Task<PaymentResult> GetAsync(Guid paymentId);

        Task<IEnumerable<PaymentResult>> BrowseAsync(Guid merchantId);
    }
}
