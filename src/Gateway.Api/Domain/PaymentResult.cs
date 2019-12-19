using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Api.Domain
{
    public class PaymentResult
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ProcessedAt { get; set; }
        public bool Processed { get; set; }
        public string AcquirerStatus { get; set; }
        public string ErrorCode { get; set; }
    }
}
