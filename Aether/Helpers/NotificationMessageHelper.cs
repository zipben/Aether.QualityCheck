using Aether.Extensions;
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

        private static void ValidateArguments(string templateId, string stage, string applicationId, string from, string subject, string body, List<string> toEmailList)
        {
            if (!templateId.Exists()) throw new ArgumentNullException($"{nameof(templateId)} field must be defined");
            if (!stage.Exists()) throw new ArgumentNullException($"{nameof(stage)} field must be defined");
            if (!applicationId.Exists()) throw new ArgumentNullException($"{nameof(applicationId)} field must be defined");
            if (!from.Exists()) throw new ArgumentNullException($"{nameof(from)} field must be defined");
            if (!subject.Exists()) throw new ArgumentNullException($"{nameof(subject)} field must be defined");
            if (!body.Exists()) throw new ArgumentNullException($"{nameof(body)} field must be defined");
            if (toEmailList.IsNullOrEmpty()) throw new ArgumentNullException($"{nameof(toEmailList)} must have at least one recipient");
        }

        private static void ValidateArguments(string templateId, string stage, string applicationId, string from, string subject, List<string> toEmailList, params KeyValuePair<string, string> [] bodyParams)
        {
            if (!templateId.Exists()) throw new ArgumentNullException($"{nameof(templateId)} field must be defined");
            if (!stage.Exists()) throw new ArgumentNullException($"{nameof(stage)} field must be defined");
            if (!applicationId.Exists()) throw new ArgumentNullException($"{nameof(applicationId)} field must be defined");
            if (!from.Exists()) throw new ArgumentNullException($"{nameof(from)} field must be defined");
            if (!subject.Exists()) throw new ArgumentNullException($"{nameof(subject)} field must be defined");
            if (bodyParams.IsNullOrEmpty()) throw new ArgumentNullException($"{nameof(bodyParams)} field must be defined");
            if (bodyParams.Any(l => string.IsNullOrEmpty(l.Value))) throw new ArgumentNullException($"{nameof(bodyParams)} value in KVP must be defined");
            if (toEmailList.IsNullOrEmpty()) throw new ArgumentNullException($"{nameof(toEmailList)} must have at least one recipient");
        }

    }
}
