using System;
using System.Threading.Tasks;
using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models.NotificationService;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
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
            _apiLogger =                    Guard.Against.Null(apiLogger, nameof(apiLogger));
            _notificationServiceClient =    Guard.Against.Null(notificationServiceClient, nameof(notificationServiceClient));
            _notificationMessageHelper =    Guard.Against.Null(notificationMessageHelper, nameof(notificationMessageHelper));
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

            Guard.Against.NullOrWhiteSpace(email.Body, nameof(email.Body));
        }
    }
}
