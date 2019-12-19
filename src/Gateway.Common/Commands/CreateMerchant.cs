using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Commands
{
    public class CreateMerchant : ICommand
    {
        public string Login { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string AcquiringBank { get; set; }

    }
}
