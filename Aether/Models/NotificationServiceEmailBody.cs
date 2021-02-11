using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public class NotificationServiceEmailBody
    {
        public class EmailRootObject
        {
            public string templateId { get; set; }
            public string stage { get; set; }
            public string applicationId { get; set; }
            public string notificationType { get; set; }
            public List<Attachment> attachments { get; set; }
            public SendParameters sendParameters { get; set; }
            public Subjectparameters subjectParameters { get; set; }
            public Bodyparameters bodyParameters { get; set; }
        }

        public class SendParameters
        {
            public string[] to { get; set; }
            public string from { get; set; }
            public string[] cc { get; set; }
            public string[] bcc { get; set; }
            public string[] overrideEmail { get; set; }
        }

        public class Subjectparameters
        {
            public string messageToReplace { get; set; }
        }

        public class Bodyparameters
        {
            public string thisParameter { get; set; }
            public string thatParameter { get; set; }
        }

        public class Attachment
        {
            public string Content { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public string Cid { get; set; }
        }
    }
}
