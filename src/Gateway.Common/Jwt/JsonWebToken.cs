using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Common.Jwt
{
    public class JsonWebToken
    {
        public string Token { get; set; }

        public long Expires { get; set; }
    }
}
