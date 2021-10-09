using InventoryStockWatch.Core.Interfaces.Repositories;
using InventoryStockWatch.Core.Interfaces.Services;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace InventoryStockWatch.Core.Repositories.Communication
{
    public class TwilioCommunicationRepository : ICommunicationRepository<SMSMessage>
    {
        private readonly IConfigurationService _configuration;

        public TwilioCommunicationRepository(IConfigurationService configuration)
        {
            _configuration = configuration;

            if (configuration.GetCommunicationMethod() is not CommunicationMethod.SMS)
                return;

            TwilioClient.Init(_configuration.GetTwilioAccountSid(), _configuration.GetTwilioAccountSecret());
        }

        public async Task SendAsync(SMSMessage payload)
        {
            await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(payload.To))
            {
                From = _configuration.GetTwilioFromNumber(),
                Body = payload.Message
            });
        }
    }
}
