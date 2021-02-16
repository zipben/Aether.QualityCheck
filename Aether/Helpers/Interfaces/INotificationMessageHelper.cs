using System;
using System.Collections.Generic;
using System.Text;
using static Aether.Models.NotificationServiceEmailBody;

namespace Aether.Helpers.Interfaces
{
    public interface INotificationMessageHelper
    {
        public EmailRootObject CreateEmail(string templateId, string stage, string applicationId, string fromEmail, string subject, string body, List<string> toEmailList, List<string> ccEmailList = null);
    }
}
