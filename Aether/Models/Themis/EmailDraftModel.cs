using System;

namespace Aether.Models.Themis
{
    public class EmailDraftModel
    {
        public string CaseName { get; set; }
        public DateTime DateDraftUpdated { get; set; }
        public string DraftContent { get; set; }
    }
}
