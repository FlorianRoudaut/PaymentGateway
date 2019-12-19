using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Commands
{
    public interface IAcquirerCommand: ICommand
    {
        string MerchantName { get; set; }

        string AquirerName { get; }
    }
}
