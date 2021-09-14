using System;
using System.Collections.Generic;
using Aether.Interfaces.Themis;

namespace Aether.Models.Themis
{
    public class Hold : IHold
    {
        public string Id { get; set; }
        public string HoldName { get; set; }
        public bool IsTest { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public EmailDraftModel EmailDraft { get; set; }
        public List<EmailDetailModel> Emails { get; set; }
    }
}
