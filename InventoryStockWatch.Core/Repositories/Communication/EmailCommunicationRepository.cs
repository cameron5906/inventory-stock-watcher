using InventoryStockWatch.Core.Interfaces.Repositories;
using InventoryStockWatch.Core.Interfaces.Services;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Repositories.Communication
{
    public class EmailCommunicationRepository : ICommunicationRepository<Email>
    {
        private readonly IConfigurationService _configuration;
        private readonly SmtpClient _smtpClient;

        public EmailCommunicationRepository(IConfigurationService configuration)
        {
            _configuration = configuration;

            if (configuration.GetCommunicationMethod() is not CommunicationMethod.Email)
                return;

            _smtpClient = new SmtpClient(configuration.GetEmailHost(), configuration.GetEmailPort());
            _smtpClient.Credentials = new NetworkCredential(configuration.GetEmailUsername(), configuration.GetEmailPassword());
        }

        public async Task SendAsync(Email payload)
        {
            var email = new MailMessage
            {
                From = new MailAddress(_configuration.GetEmailFromAddress()),
                Subject = payload.Subject,
                Body = payload.Message
            };
            email.To.Add(payload.To);

            await _smtpClient.SendMailAsync(email);
        }
    }
}
