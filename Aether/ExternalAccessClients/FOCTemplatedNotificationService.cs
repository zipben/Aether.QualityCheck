using System;
using System.Linq;
using System.Threading.Tasks;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models.NotificationService;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using static Aether.Models.NotificationService.NotificationServiceEmailBody;

namespace Aether.ExternalAccessClients
{
    public class FOCTemplatedNotificationService : BaseFOCNotificationService, IFOCTemplatedNotificationService
    {
        private readonly IApiLogger _apiLogger;
        private readonly INotificationServiceClient _notificationServiceClient;
        private readonly INotificationMessageHelper _notificationMessageHelper;

        public FOCTemplatedNotificationService(IApiLogger apiLogger,
                                      INotificationServiceClient notificationServiceClient,
                                      INotificationMessageHelper notificationMessageHelper)
        {
            _apiLogger =                    Guard.Against.Null(apiLogger, nameof(apiLogger));
            _notificationServiceClient =    Guard.Against.Null(notificationServiceClient, nameof(notificationServiceClient));
            _notificationMessageHelper =    Guard.Against.Null(notificationMessageHelper, nameof(notificationMessageHelper));
        }

        public async Task<bool> SendEmailAsync(TemplatedEmailSendModel email)
        {
            _apiLogger.LogInfo($"{nameof(FOCTemplatedNotificationService)}:{nameof(SendEmailAsync)}");

            ValidateEmailSendModel(email);
            EmailRootObject emailObj = _notificationMessageHelper.CreateEmail(email.TemplateId, email.Stage, email.ApplicationId, email.From, email.Subject, email.To, email.CC, email.BCC, email.Contents.ToArray());

            return await _notificationServiceClient.TryPostRequestAsync(emailObj);
        }

        private void ValidateEmailSendModel(TemplatedEmailSendModel email)
        {
            base.ValidateBaseEmailSendModel(email);

            Guard.Against.NullOrEmpty(email.Contents, nameof(email.Contents));
        }
    }
}
