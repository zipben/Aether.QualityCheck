using System.Collections.Generic;

namespace Aether.Models.NotificationService
{
    public abstract class BaseEmailSendModel
    {
        public string TemplateId { get; set; }
        public string Stage { get; set; }
        public string ApplicationId { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string Subject { get; set; }
    }
}
