using PiggyBankApi.Model;
using System;
using System.Threading.Tasks;

namespace PiggyBankApi.Functions
{
    public static class ApiFunctions
    {
        public async static Task<PiggyPaymentStatus> ProcessPaymentRequestAsync(PiggyPaymentRequest payment)
        {
            var task = Task.Run(() => ProcessPaymentRequest(payment));
            return await task;
        }

        public static PiggyPaymentStatus ProcessPaymentRequest(PiggyPaymentRequest payment)
        {
            if (string.IsNullOrWhiteSpace(payment.CardNumber) && string.IsNullOrWhiteSpace(payment.Cvv))
            {
                var status = new PiggyPaymentStatus();
                status.Status = PiggyStatus.Failed;
                return status;
            }
            else
            {
                var status = new PiggyPaymentStatus();
                status.PaymentId = Guid.NewGuid();
                status.Status = PiggyStatus.Authorised;
                return status;
            }
        }
    }
}
