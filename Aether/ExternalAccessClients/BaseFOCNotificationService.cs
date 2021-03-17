using Aether.Extensions;
using Aether.Models;
using System;

namespace Aether.ExternalAccessClients
{
    public abstract class BaseFOCNotificationService
    {
        internal void ValidateBaseEmailSendModel(BaseEmailSendModel email)
        {
            if (!email.TemplateId.Exists()) throw new ArgumentNullException(nameof(email.TemplateId));
            if (!email.Stage.Exists()) throw new ArgumentNullException(nameof(email.Stage));
            if (!email.ApplicationId.Exists()) throw new ArgumentNullException(nameof(email.ApplicationId));
            if (!email.From.Exists()) throw new ArgumentNullException(nameof(email.From));
            if (!email.Subject.Exists()) throw new ArgumentNullException(nameof(email.Subject));
            if (email.To.IsNullOrEmpty()) throw new ArgumentNullException(nameof(email.To));
        }
    }
}
