using Gateway.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Logging.Model
{
    public class Log
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public LogLevel LogLevel { get; set; }
        public string LogCode { get; set; }
        public string LogMessage { get; set; }
    }
}
