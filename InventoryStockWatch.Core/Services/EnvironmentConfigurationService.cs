using InventoryStockWatch.Core.Interfaces.Services;
using InventoryStockWatch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Services
{
    public class EnvironmentConfigurationService : IConfigurationService
    {
        private const string ProductDataPath = "PRODUCT_JSON_PATH";
        private const string CommunicationMethod = "COMMUNICATION_METHOD";
        private const string AlertRecipient = "ALERT_RECIPIENT";
        private const string EmailFromAddress = "SMTP_FROM_ADDRESS";
        private const string EmailHost = "SMTP_HOST";
        private const string EmailPort = "SMTP_PORT";
        private const string EmailUsername = "SMTP_USERNAME";
        private const string EmailPassword = "SMTP_PASSWORD";
        private const string TwilioAccountSid = "TWILIO_ACCOUNT_SID";
        private const string TwilioAccountSecret = "TWILIO_ACCOUNT_SECRET";
        private const string TwilioFromNumber = "TWILIO_FROM_NUMBER";

        public CommunicationMethod GetCommunicationMethod()
        {
            if (!Enum.TryParse<CommunicationMethod>(Environment.GetEnvironmentVariable(CommunicationMethod), out var communicationMethod))
                throw new Exception($"{CommunicationMethod} is not configured");

            return communicationMethod;
        }

        public string GetAlertRecipient()
        {
            return GetEnvironmentVariableOrThrow(AlertRecipient);
        }

        public string GetEmailFromAddress()
        {
            return GetEnvironmentVariableOrThrow(EmailFromAddress);
        }

        public string GetEmailHost()
        {
            return GetEnvironmentVariableOrThrow(EmailHost);
        }

        public string GetEmailPassword()
        {
            return GetEnvironmentVariableOrThrow(EmailPassword);
        }

        public int GetEmailPort()
        {
            var portStr = GetEnvironmentVariableOrThrow(EmailPort);

            if (!int.TryParse(portStr, out var port))
                throw new Exception($"{EmailPort} must be a valid port number");

            return port;
        }

        public string GetEmailUsername()
        {
            return GetEnvironmentVariableOrThrow(EmailUsername);
        }

        public string GetProductDataPath()
        {
            return GetEnvironmentVariableOrThrow(ProductDataPath);
        }

        public string GetTwilioAccountSecret()
        {
            return GetEnvironmentVariableOrThrow(TwilioAccountSecret);
        }

        public string GetTwilioAccountSid()
        {
            return GetEnvironmentVariableOrThrow(TwilioAccountSid);
        }

        public string GetTwilioFromNumber()
        {
            return GetEnvironmentVariableOrThrow(TwilioFromNumber);
        }

        private string GetEnvironmentVariableOrThrow(string name)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
                throw new Exception($"{name} is not configured");

            return Environment.GetEnvironmentVariable(name);
        }
    }
}
