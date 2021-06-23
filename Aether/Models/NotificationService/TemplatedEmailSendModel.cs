using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.NotificationService
{
    public class TemplatedEmailSendModel : BaseEmailSendModel
    {
        public Dictionary<string, string> Contents { get; set; }
    }
}
