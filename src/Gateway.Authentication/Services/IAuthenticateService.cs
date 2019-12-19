using Gateway.Common.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Authentication.Services
{
    public interface IAuthenticateService
    {

        Task<JsonWebToken> LoginAsync(string login, string password);

        Task RegisterAsync(string login, string password, string name, string acquiringBank);
    }
}
