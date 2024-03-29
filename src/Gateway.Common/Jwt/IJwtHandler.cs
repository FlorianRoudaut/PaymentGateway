﻿using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Common.Jwt
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}
