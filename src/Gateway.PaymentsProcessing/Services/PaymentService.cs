using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Common.Commands;
using Gateway.Common.Model;
using Gateway.PaymentsProcessing.Repositories;

namespace Gateway.PaymentsProcessing.Services
{
    public class PaymentService : IPaymentService
    {
        private IMerchantRepository _merchantRepository { get; }

        public PaymentService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }


        public async Task<IAcquirerCommand> ProcessAsync(ProcessPayment command)
        {
            var merchant = await _merchantRepository.GetByNameAsync(command.MerchantName);
            if(merchant==null)
            {
                throw new Exception("Cannot find merchant "+command.MerchantName);
            }

            if(merchant.AcquiringBank=="PiggyBank")
            {
                var acquirerCommand = new ProcessPiggyPayment();
                command.CopyPayment(acquirerCommand);
                return acquirerCommand;
            }
            else
            {
                throw new Exception("Merchant not supported " + command.MerchantName);
            }
        }
    }
}
