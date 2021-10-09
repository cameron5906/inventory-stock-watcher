using InventoryStockWatch.Core.Interfaces.Repositories;
using InventoryStockWatch.Core.Interfaces.Services;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Services
{
    public class CommunicationService
    {
        private readonly IConfigurationService _configuration;
        private readonly ICommunicationRepository<SMSMessage> _smsRepository;
        private readonly ICommunicationRepository<Email> _emailRepository;

        public CommunicationService(IConfigurationService configuration, ICommunicationRepository<SMSMessage> smsRepository, ICommunicationRepository<Email> emailRepository)
        {
            _configuration = configuration;
            _smsRepository = smsRepository;
            _emailRepository = emailRepository;
        }

        public async Task SendMessageAsync(string to, string message, string subject = "")
        {
            switch(_configuration.GetCommunicationMethod())
            {
                case CommunicationMethod.Email:
                    await _emailRepository.SendAsync(new Email
                    {
                        To = to,
                        Message = message,
                        Subject = subject
                    });
                    break;
                case CommunicationMethod.SMS:
                    await _smsRepository.SendAsync(new SMSMessage
                    {
                        To = to,
                        Message = message
                    });
                    break;
            }
        }
    }
}
