using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Common.Acquirers;
using Gateway.Common.Commands;
using Gateway.Common.Events;
using Gateway.Common.Model;
using PiggyBankApi.Functions;
using PiggyBankApi.Model;

namespace Gateay.PiggyConnector.Service
{
    public class PiggyService : IAcquirerService
    {
        public async Task<PaymentProcessed> ProcessAsync(IAcquirerCommand processPayment)
        {
            var piggyPayment = processPayment as ProcessPiggyPayment;
            if(piggyPayment==null)
            {
                throw new Exception("Invalid payment type");
            }
            var piggyRequest = BuildPiggyRequest(piggyPayment);
            var piggyStatus = await ApiFunctions.ProcessPaymentRequestAsync(piggyRequest);
            return BuildPaymentProcessed(piggyPayment, piggyStatus);
        }

        private static PiggyPaymentRequest BuildPiggyRequest(ProcessPiggyPayment processPayment)
        {
            var piggyRequest = new PiggyPaymentRequest();
            piggyRequest.CardNumber = processPayment.CardNumber;
            piggyRequest.Cvv = processPayment.Cvv;
            piggyRequest.ExpiryMonth = processPayment.ExpiryMonth;
            piggyRequest.ExpiryYear = processPayment.ExpiryYear;
            piggyRequest.From = processPayment.CardHolderName;
            piggyRequest.To = processPayment.MerchantName;
            piggyRequest.Amount = processPayment.Amount;
            piggyRequest.Currency = processPayment.Currency;
            return piggyRequest;
        }

        private static PaymentProcessed BuildPaymentProcessed(ProcessPiggyPayment processPayment, PiggyPaymentStatus status)
        {
            var paymentProcessed = new PaymentProcessed();
            processPayment.CopyPayment(paymentProcessed);
            paymentProcessed.ProcessedAt = DateTime.UtcNow;
            paymentProcessed.Status = status.Status.ToString();
            paymentProcessed.AcquirerPaymentId = status.PaymentId;
            return paymentProcessed;
        }
    }
}
