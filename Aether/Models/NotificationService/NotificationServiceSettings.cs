using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.NotificationService
{
    public class NotificationServiceSettings
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string BaseUrl { get; set; }
        public string EndPoint { get; set; }
        public string Audience { get; set; }
    }
}
