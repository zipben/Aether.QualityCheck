using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Aether.Models.NotificationService.NotificationServiceEmailBody;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface INotificationServiceClient
    {
        Task<bool> TryPostRequestAsync(EmailRootObject emailObj);
    }
}
