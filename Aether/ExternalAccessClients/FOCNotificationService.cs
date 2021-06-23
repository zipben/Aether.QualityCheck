using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models;
using Aether.Models.NotificationService;
using APILogger.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Aether.Models.NotificationService.NotificationServiceEmailBody;

namespace Aether.ExternalAccessClients
{
    public class FOCNotificationService : BaseFOCNotificationService, IFOCNotificationService
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
            _apiLogger.LogInfo($"{nameof(FOCNotificationService)}:{nameof(SendEmailAsync)}");

            ValidateEmailSendModel(email);
            EmailRootObject emailObj = _notificationMessageHelper.CreateEmail(email.TemplateId, email.Stage, email.ApplicationId, email.From, email.Subject, email.Body, email.To, email.CC);

            return await _notificationServiceClient.TryPostRequestAsync(emailObj);
        }

        private void ValidateEmailSendModel(EmailSendModel email)
        {
            base.ValidateBaseEmailSendModel(email);

            if (!email.Body.Exists()) throw new ArgumentNullException(nameof(email.Body));
        }
    }
}
