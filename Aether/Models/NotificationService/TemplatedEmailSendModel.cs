using System.Collections.Generic;

namespace Aether.Models.NotificationService
{
    public class TemplatedEmailSendModel : BaseEmailSendModel
    {
        public Dictionary<string, string> Contents { get; set; }
    }
}
