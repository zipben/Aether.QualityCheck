using System;
using System.Collections.Generic;

namespace Aether.Models.Themis
{
    public class EmailDetailModel
    {
        public string Id { get; set; }
        public string HoldName { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Guid? EmailConfirmationId { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}
