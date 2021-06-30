using Aether.Models.NotificationService;
using Ardalis.GuardClauses;

namespace Aether.ExternalAccessClients
{
    public abstract class BaseFOCNotificationService
    {
        protected void ValidateBaseEmailSendModel(BaseEmailSendModel email)
        {
            Guard.Against.NullOrWhiteSpace(email.TemplateId, nameof(email.TemplateId));
            Guard.Against.NullOrWhiteSpace(email.Stage, nameof(email.Stage));
            Guard.Against.NullOrWhiteSpace(email.ApplicationId, nameof(email.ApplicationId));
            Guard.Against.NullOrWhiteSpace(email.From, nameof(email.From));
            Guard.Against.NullOrWhiteSpace(email.Subject, nameof(email.Subject));
            Guard.Against.NullOrEmpty(email.To, nameof(email.To));
        }
    }
}
