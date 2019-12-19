using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Authentication.Services
{
    public interface IEncrypter
    {
        string GetSalt();

        string GetHash(string value, string salt);

        bool ValidatePassword(string savedHash, string salt, string attempt);
    }
}
