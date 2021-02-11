using Aether.Helpers.Interfaces;
using Aether.Models;
using APILogger.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using static Aether.Models.NotificationServiceEmailBody;

namespace Aether.Helpers
{
    public class NotificationMessageHelper : INotificationMessageHelper
    {
        private readonly IApiLogger _apiLogger;
        private readonly NotificationServiceHelperSettings _notificationServiceSettings;

        public NotificationMessageHelper(IApiLogger apiLogger, IOptions<NotificationServiceHelperSettings> notificationServiceSettings)
        {
            _apiLogger = apiLogger ?? throw new ArgumentNullException(nameof(apiLogger));
            _notificationServiceSettings = notificationServiceSettings?.Value ?? throw new ArgumentNullException(nameof(notificationServiceSettings));
        }

        public EmailRootObject CreateEmail(string fromEmail, string subject, string body, List<string> toEmailList)
        {
            ValidateArguments(fromEmail, subject, body, toEmailList);

            return new EmailRootObject
            {
                templateId = _notificationServiceSettings.ApplicationId,
                stage = _notificationServiceSettings.Stage,
                applicationId = _notificationServiceSettings.ApplicationId,
                notificationType = _notificationServiceSettings.NotificationType,
                subjectParameters = new Subjectparameters { messageToReplace = subject },
                bodyParameters = new Bodyparameters { thisParameter = body },
                sendParameters = new SendParameters
                {
                    from = fromEmail,
                    to = toEmailList.ToArray(),
                },
            };
        }

        private static void ValidateArguments(string from, string subject, string body, List<string> toEmailList)
        {
            if (string.IsNullOrWhiteSpace(from)) throw new ArgumentNullException($"{nameof(from)} field must be defined");
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentNullException($"{nameof(subject)} field must be defined");
            if (string.IsNullOrWhiteSpace(body)) throw new ArgumentNullException($"{nameof(body)} field must be defined");
            if (toEmailList == null || !toEmailList.Any()) throw new ArgumentNullException($"{nameof(toEmailList)} must have at least one recipient");
        }
    }
}
