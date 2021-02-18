using Aether.Extensions;
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
            ValidateEmailSendModel(email);
            EmailRootObject emailObj = _notificationMessageHelper.CreateEmail(email.TemplateId, email.Stage, email.ApplicationId, email.From, email.Subject, email.Body, email.To, email.CC);

            return await _notificationServiceClient.TryPostRequestAsync(emailObj);
        }

        private static void ValidateEmailSendModel(EmailSendModel email)
        {
            if (!email.TemplateId.Exists()) throw new ArgumentNullException(nameof(email.TemplateId));
            if (!email.Stage.Exists()) throw new ArgumentNullException(nameof(email.Stage));
            if (!email.ApplicationId.Exists()) throw new ArgumentNullException(nameof(email.ApplicationId));
            if (!email.From.Exists()) throw new ArgumentNullException(nameof(email.From));
            if (!email.Subject.Exists()) throw new ArgumentNullException(nameof(email.Subject));
            if (!email.Body.Exists()) throw new ArgumentNullException(nameof(email.Body));
            if (email.To.IsNullOrEmpty()) throw new ArgumentNullException(nameof(email.To));
        }
    }
}
