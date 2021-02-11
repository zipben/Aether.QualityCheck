using Aether.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface IFOCNotificationService
    {
        Task<bool> SendEmailAsync(EmailSendModel email);
    }
}
