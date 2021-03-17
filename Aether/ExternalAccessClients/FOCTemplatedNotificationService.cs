using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models;
using APILogger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aether.Models.NotificationServiceEmailBody;

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
            _apiLogger = apiLogger ?? throw new ArgumentNullException(nameof(apiLogger));
            _notificationServiceClient = notificationServiceClient ?? throw new ArgumentNullException(nameof(notificationServiceClient));
            _notificationMessageHelper = notificationMessageHelper ?? throw new ArgumentNullException(nameof(notificationMessageHelper));
        }

        public async Task<bool> SendEmailAsync(TemplatedEmailSendModel email)
        {
            _apiLogger.LogInfo($"{nameof(FOCTemplatedNotificationService)}:{nameof(SendEmailAsync)}");

            ValidateEmailSendModel(email);
            EmailRootObject emailObj = _notificationMessageHelper.CreateEmail(email.TemplateId, email.Stage, email.ApplicationId, email.From, email.Subject, email.To, email.CC, email.Contents.ToArray());

            return await _notificationServiceClient.TryPostRequestAsync(emailObj);
        }

        private void ValidateEmailSendModel(TemplatedEmailSendModel email)
        {
            base.ValidateBaseEmailSendModel(email);

            if (email.Contents is null) throw new ArgumentNullException(nameof(email.Contents));
            if (!email.Contents.Any()) throw new ArgumentNullException($"{nameof(email.Contents)} Values");
        }
    }
}
