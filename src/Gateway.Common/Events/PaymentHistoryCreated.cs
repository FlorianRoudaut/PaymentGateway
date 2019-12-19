using Gateway.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Events
{
    public class PaymentHistoryCreated : IEvent, IPayment
    {
        public Guid AcquirerPaymentId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public bool Processed { get; set; }
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        #region Payment
        public Guid PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string MerchantName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardHolderName { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        #endregion Payment
    }
}
