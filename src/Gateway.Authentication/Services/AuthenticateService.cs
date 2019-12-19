using Gateway.Common.Jwt;
using Gateway.Common.Commands;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Authentication.Repositories;
using Gateway.Authentication.Domain;
using Gateway.Common.Events;

namespace Gateway.Authentication.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private IBusClient _busClient { get; }
        private IUserRepository _userRepository { get; }
        private IEncrypter _encrypter { get; }

        private IJwtHandler _jwtHandler { get; }
        public AuthenticateService(IUserRepository userRepository,
            IEncrypter encrypter, IJwtHandler jwtHandler, IBusClient busClient)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _encrypter = encrypter;
            _busClient = busClient;
        }

        public async Task<JsonWebToken> LoginAsync(string login, string password)
        {
            var merchant = await _userRepository.GetByLoginAsync(login);
            if (merchant == null)
            {
                var message = "Cannot find login " + login;
                var logCreated = new CreateLog(Guid.Empty, Guid.Empty, LogLevel.Warning, "Invalid_Credentials",
                    message);
                await _busClient.PublishAsync(logCreated);
                throw new Exception(message);
            }
            else if (!_encrypter.ValidatePassword(merchant.PasswordHash, merchant.Salt, password))
            {
                var message = "Invalid password " + password + " for login " + login;

                var logCreated = new CreateLog(Guid.Empty, Guid.Empty, LogLevel.Warning, "Invalid_Credentials",
                    message);
                await _busClient.PublishAsync(logCreated);
                throw new Exception(message);
            }
            else
            {
                var logCreated = new CreateLog(Guid.Empty, Guid.Empty, LogLevel.Warning, "Login",
                    "User " + login + " authenticated");
                await _busClient.PublishAsync(logCreated);
            }

            return _jwtHandler.Create(merchant.Id);
        }

        public async Task RegisterAsync(string login, string password, string name, string acquiringBank)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user != null)
            {
                var message = "User " + login + " already exists";
                var logCreated = new CreateLog(Guid.Empty, Guid.Empty, LogLevel.Warning, "Duplicate_Login",
                    message);
                await _busClient.PublishAsync(logCreated);
                throw new Exception(message);
            }

            user = new User(login, name);

            if (string.IsNullOrWhiteSpace(password))
            {
                var message = "Password cannot be empty for user " + login;
                var logCreated = new CreateLog(Guid.Empty, Guid.Empty, LogLevel.Warning, "Empty_Password",
                    message);
                await _busClient.PublishAsync(logCreated);
                throw new Exception(message);
            }

            user.Salt = _encrypter.GetSalt();
            user.PasswordHash = _encrypter.GetHash(password, user.Salt);

            await _userRepository.AddAsync(user);

            var logCreated2 = new CreateLog(Guid.Empty, Guid.Empty, LogLevel.Warning, "Created_User",
                    "User " + login + " created");
            await _busClient.PublishAsync(logCreated2);

            var userCreated = new UserCreated();
            userCreated.UserId = user.Id;
            userCreated.Name = user.Name;
            userCreated.AcquirerName = acquiringBank;
            await _busClient.PublishAsync(userCreated);
        }
    }
}
