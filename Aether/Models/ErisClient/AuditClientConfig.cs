using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.ErisClient
{
    public class AuditClientConfig
    {
        public string BaseUrl { get; set; }
        public string Audience { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }
}
