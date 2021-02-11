using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public class EmailSendModel
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
