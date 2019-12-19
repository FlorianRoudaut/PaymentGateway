using Gateway.Common.Model;
using Gateway.History.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.History.Repositories
{
    public interface IPaymentHistoryRepository
    {
        public Task AddAsync(PaymentHistory history);
    }
}
