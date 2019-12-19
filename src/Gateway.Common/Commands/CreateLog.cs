using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Commands
{
    public enum LogLevel { Critical, Error, Warning, Info, Debug };

    public class CreateLog : ICommand
    {
        public Guid MerchantId { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public LogLevel LogLevel { get; set; }
        public string LogCode { get; set; }
        public string LogMessage { get; set; }

        protected CreateLog() { }

        public CreateLog(Guid merchantId, Guid paymentId, LogLevel logLevel, string logCode, string logMessage)
        {
            MerchantId = merchantId;
            PaymentId = paymentId;
            LogLevel = logLevel;
            LogCode = logCode;
            LogMessage = logMessage;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
