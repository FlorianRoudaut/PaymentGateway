using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Model
{
    public static class PaymentExtensions
    {
        public static string ToString(this IPayment payment)
        {
            return "From :" + payment.CardHolderName + "|To:" + payment.MerchantName + "|Amount:"
                + payment.Amount.ToString("n2") + " " + payment.Currency;
        }

        public static void CopyPayment(this IPayment from, IPayment to)
        {
            to.PaymentId = from.PaymentId;
            to.CreatedAt = from.CreatedAt;
            to.UserId = from.UserId;
            to.MerchantName = from.MerchantName;
            to.CardNumber = from.CardNumber;
            to.Cvv = from.Cvv;
            to.ExpiryMonth = from.ExpiryMonth;
            to.ExpiryYear = from.ExpiryYear;
            to.CardHolderName = from.CardHolderName;
            to.Amount = from.Amount;
            to.Currency = from.Currency;
        }
    }
}
