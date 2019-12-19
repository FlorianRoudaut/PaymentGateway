using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Common.Commands
{
    public class AuthenticateMerchant : ICommand
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
