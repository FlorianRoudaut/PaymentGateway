using Gateway.Api.Domain;
using Gateway.Api.Repositories;
using Gateway.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Api.Services
{
    public class PaymentResultService : IPaymentResultService
    {
        private IPaymentResultRepository _resultRepository { get; }

        public PaymentResultService(IPaymentResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task SaveAsync(PaymentHistoryCreated @event)
        {
            var paymentHistory = new PaymentResult();
            paymentHistory.Id = @event.PaymentId;
            paymentHistory.MerchantId = @event.UserId;
            paymentHistory.CardHolderName = @event.CardHolderName;
            paymentHistory.Amount = @event.Amount;
            paymentHistory.Currency = @event.Currency;
            paymentHistory.CreatedAt = @event.CreatedAt;
            paymentHistory.ProcessedAt = @event.ProcessedAt;
            paymentHistory.Processed = @event.Processed;
            paymentHistory.AcquirerStatus = @event.Status; 
            paymentHistory.ErrorCode = @event.ErrorCode;

            await _resultRepository.AddAsync(paymentHistory);
        }


        public async Task<PaymentResult> GetAsync(Guid paymentId)
        {
            return await _resultRepository.GetAsync(paymentId);
        }

        public async Task<IEnumerable<PaymentResult>> BrowseAsync(Guid merchantId)
        {
            return await _resultRepository.BrowseAsync(merchantId);
        }
    }
}
