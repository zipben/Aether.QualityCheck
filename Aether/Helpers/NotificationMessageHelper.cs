using Aether.Helpers.Interfaces;
using APILogger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static Aether.Models.NotificationServiceEmailBody;

namespace Aether.Helpers
{
    public class NotificationMessageHelper : INotificationMessageHelper
    {
        private readonly IApiLogger _apiLogger;

        public NotificationMessageHelper(IApiLogger apiLogger)
        {
            _apiLogger = apiLogger ?? throw new ArgumentNullException(nameof(apiLogger));
        }

        public EmailRootObject CreateEmail(string templateId, string stage, string applicationId, string fromEmail, string subject, string body, List<string> toEmailList, List<string> ccEmailList = null)
        {
            ValidateArguments(templateId, stage, applicationId, fromEmail, subject, body, toEmailList);

            return new EmailRootObject
            {
                templateId = templateId,
                stage = stage,
                applicationId = applicationId,
                notificationType = "email",
                subjectParameters = new Subjectparameters { messageToReplace = subject },
                bodyParameters = new Bodyparameters { thisParameter = body },
                sendParameters = new SendParameters
                {
                    from = fromEmail,
                    to = toEmailList.ToArray(),
                    cc = ccEmailList != null ? ccEmailList.ToArray():null
                },
            };
        }

        private static void ValidateArguments(string templateId, string stage, string applicationId, string from, string subject, string body, List<string> toEmailList)
        {
            if (string.IsNullOrEmpty(templateId)) throw new ArgumentNullException($"{nameof(templateId)} field must be defined");
            if (string.IsNullOrEmpty(stage)) throw new ArgumentNullException($"{nameof(stage)} field must be defined");
            if (string.IsNullOrEmpty(applicationId)) throw new ArgumentNullException($"{nameof(applicationId)} field must be defined");
            if (string.IsNullOrWhiteSpace(from)) throw new ArgumentNullException($"{nameof(from)} field must be defined");
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentNullException($"{nameof(subject)} field must be defined");
            if (string.IsNullOrWhiteSpace(body)) throw new ArgumentNullException($"{nameof(body)} field must be defined");
            if (toEmailList == null || !toEmailList.Any()) throw new ArgumentNullException($"{nameof(toEmailList)} must have at least one recipient");
        }
    }
}
