using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models;
using APILogger.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Aether.Models.NotificationServiceEmailBody;

namespace Aether.ExternalAccessClients
{
    public class FOCNotificationService : IFOCNotificationService
    {
        private readonly IApiLogger _apiLogger;
        private readonly INotificationServiceClient _notificationServiceClient;
        private readonly INotificationMessageHelper _notificationMessageHelper;

        public FOCNotificationService(IApiLogger apiLogger,
                                      INotificationServiceClient notificationServiceClient,
                                      INotificationMessageHelper notificationMessageHelper)
        {
            _apiLogger = apiLogger ?? throw new ArgumentNullException(nameof(apiLogger));
            _notificationServiceClient = notificationServiceClient ?? throw new ArgumentNullException(nameof(notificationServiceClient));
            _notificationMessageHelper = notificationMessageHelper ?? throw new ArgumentNullException(nameof(notificationMessageHelper));
        }

        public async Task<bool> SendEmailAsync(EmailSendModel email)
        {
            EmailRootObject emailObj = _notificationMessageHelper.CreateEmail(email?.From, email?.Subject, email?.Body, email?.To);

            return await _notificationServiceClient.TryPostRequestAsync(emailObj);
        }
    }
}
