using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Events
{
    public interface IEvent
    {
        Guid UserId { get; }
    }
}
