using System;
using System.Collections.Generic;
using System.Text;

namespace PiggyBankApi.Model
{
    public class PiggyPaymentRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
    }
}
