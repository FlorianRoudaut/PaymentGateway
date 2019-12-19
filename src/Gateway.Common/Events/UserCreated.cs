using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Events
{
    public class UserCreated : IEvent
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string AcquirerName { get; set; }
    }
}
