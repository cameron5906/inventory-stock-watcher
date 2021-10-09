using InventoryStockWatch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        string GetProductDataPath();

        CommunicationMethod GetCommunicationMethod();
        string GetAlertRecipient();

        #region SMS
        string GetTwilioFromNumber();
        string GetTwilioAccountSid();
        string GetTwilioAccountSecret();
        #endregion

        #region Email
        string GetEmailHost();
        int GetEmailPort();
        string GetEmailFromAddress();
        string GetEmailUsername();
        string GetEmailPassword();
        #endregion
    }
}
