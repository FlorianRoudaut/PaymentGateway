using Gateway.Common.Commands;
using Gateway.Common.Events;
using Gateway.Common.Model;
using Gateway.PaymentsProcessing.Domain;
using Gateway.PaymentsProcessing.Repositories;
using Gateway.PaymentsProcessing.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.PaymentsProcessing.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        private IBusClient _busClient;
        private IMerchantRepository _merchantRepository;

        public UserCreatedHandler(IBusClient busClient, IMerchantRepository merchantRepository)
        {
            _busClient = busClient;
            _merchantRepository = merchantRepository;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            try
            {
                var merchant = new Merchant();
                merchant.UserId = @event.UserId;
                merchant.Name = @event.Name;
                merchant.AcquiringBank = @event.AcquirerName;

                await _merchantRepository.AddAsync(merchant);

                var logCreated = new CreateLog(@event.UserId, Guid.Empty, LogLevel.Info, "Merchant_Created",
                        "Created merchant "+@event.Name);
                await _busClient.PublishAsync(logCreated);
            }
            catch (Exception ex)
            {
                var logCreated = new CreateLog(@event.UserId, Guid.Empty, LogLevel.Error, "Merchant_Creation_Error",
                    "Cannot save Merchant  because :" + ex.Message);
                await _busClient.PublishAsync(logCreated);
            }
        }
    }
}
