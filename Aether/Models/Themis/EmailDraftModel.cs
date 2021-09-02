using System;

namespace Aether.Models.Themis
{
    public class EmailDraftModel
    {
        public string HoldName { get; set; }
        public DateTime DateDraftUpdated { get; set; }
        public string DraftContent { get; set; }
    }
}
