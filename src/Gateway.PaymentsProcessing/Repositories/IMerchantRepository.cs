using Gateway.PaymentsProcessing.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.PaymentsProcessing.Repositories
{
    public interface IMerchantRepository
    {
        Task<Merchant> GetByIdAsync(Guid id);

        Task<Merchant> GetByNameAsync(string name);

        Task AddAsync(Merchant merchant);
    }
}
