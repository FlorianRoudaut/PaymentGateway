using Gateway.Api.Domain;
using Gateway.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Api.Repositories
{
    public interface IPaymentResultRepository
    {
        public Task<PaymentResult> GetAsync(Guid id);

        public Task<IEnumerable<PaymentResult>> BrowseAsync(Guid merchantId);

        public Task AddAsync(PaymentResult history);
    }
}
