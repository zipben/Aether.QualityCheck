using Aether.Extensions;
using Aether.Helpers.Interfaces;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using static Aether.Models.NotificationService.NotificationServiceEmailBody;

namespace Aether.Helpers
{
    public class NotificationMessageHelper : INotificationMessageHelper
    {
        private readonly IApiLogger _apiLogger;

        public NotificationMessageHelper(IApiLogger apiLogger)
        {
            _apiLogger = Guard.Against.Null(apiLogger, nameof(apiLogger));
        }

        public EmailRootObject CreateEmail(string templateId, string stage, string applicationId, string fromEmail, string subject, string body, List<string> toEmailList, List<string> ccEmailList = null)
        {
            var bodyParams = new KeyValuePair<string, string>("thisParamater", body).CreateList().ToArray();

            return CreateEmail(templateId, stage, applicationId, fromEmail, subject, toEmailList, ccEmailList, bodyParams);
        }

        public EmailRootObject CreateEmail(string templateId, string stage, string applicationId, string fromEmail, string subject, List<string> toEmailList, List<string> ccEmailList = null, params KeyValuePair<string, string>[] bodyParams)
        {
            ValidateArguments(templateId, stage, applicationId, fromEmail, subject, toEmailList, bodyParams);

            return new EmailRootObject
            {
                templateId = templateId,
                stage = stage,
                applicationId = applicationId,
                notificationType = "email",
                subjectParameters = new Subjectparameters { messageToReplace = subject },
                bodyParameters = new Dictionary<string, string>(bodyParams),
                sendParameters = new SendParameters
                {
                    from = fromEmail,
                    to = toEmailList.ToArray(),
                    cc = ccEmailList?.ToArray()
                },
            };
        }

        private static void ValidateArguments(string templateId, string stage, string applicationId, string from, string subject, List<string> toEmailList, params KeyValuePair<string, string> [] bodyParams)
        {
            Guard.Against.NullOrWhiteSpace(templateId, nameof(templateId));
            Guard.Against.NullOrWhiteSpace(stage, nameof(stage));
            Guard.Against.NullOrWhiteSpace(applicationId, nameof(applicationId));
            Guard.Against.NullOrWhiteSpace(from, nameof(from));
            Guard.Against.NullOrWhiteSpace(subject, nameof(subject));
            Guard.Against.NullOrEmpty(toEmailList, nameof(toEmailList));
            Guard.Against.NullOrEmpty(bodyParams, nameof(bodyParams));
            Guard.Against.InvalidInput(bodyParams, nameof(bodyParams), x => x.All(l => !string.IsNullOrEmpty(l.Value)));
        }
    }
}
