using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Common.Events;
using Gateway.Common.Model;
using Gateway.History.Domain;
using Gateway.History.Repositories;

namespace Gateway.PaymentsProcessing.Services
{
    public class HistoryService : IHistoryService
    {
        private IPaymentHistoryRepository _historyRepository { get; }

        public HistoryService(IPaymentHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task SaveAsync(PaymentProcessed @event)
        {
            var paymentHistory = new PaymentHistory();
            paymentHistory.Id = Guid.NewGuid();
            paymentHistory.AcquirerPaymentId = @event.AcquirerPaymentId;
            paymentHistory.Processed = true;
            paymentHistory.ProcessedAt = @event.ProcessedAt;
            paymentHistory.Status = @event.Status;
            @event.CopyPayment(paymentHistory);

            await _historyRepository.AddAsync(paymentHistory);
        }

        public async Task SaveAsync(PaymentFailed @event)
        {
            var paymentHistory = new PaymentHistory();
            paymentHistory.Id = Guid.NewGuid();
            paymentHistory.Processed = false;
            paymentHistory.ErrorCode = @event.ErrorCode;
            paymentHistory.ErrorMessage = @event.ErrorMessage;
            @event.CopyPayment(paymentHistory);

            await _historyRepository.AddAsync(paymentHistory);
        }
    }
}
