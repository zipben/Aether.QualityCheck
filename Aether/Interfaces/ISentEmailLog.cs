using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces
{
    public interface ISentEmailLog
    {
        public string EnforcementRequestId { get; set; }
        public string SystemName { get; set; }
        public string EmailGuid { get; set; }
        public string EmailAddress { get; set; }
        public string EmailContent { get; set; }
        public long CreatedDate { get; set; }
        public long LastUpdatedDate { get; set; }
    }
}
