using System.Collections.Generic;
using Aether.Enums;

namespace Aether.Interfaces
{
    public interface ILedgerEntry
    {
        public string DataEnforcementRequestId { get; set; }

        public string Entity { get; set; }

        public EnforcementType RequestType { get; set; }

        public Dictionary<string, List<string>> PersonalData { get; set; }

        public bool Confirmed { get; set; }

        public bool Completed { get; set; }

        public long? ConfirmationDate { get; set; }

        public long InsertionDate { get; set; }

        ///expiration date in seconds, not ms like the other dates.
        public long ExpirationDate { get; set; }

        public long? CompletionDate { get; set; }

        public bool? IsTest { get; set; }

        public EnforcementActionType? ActionType { get; set; }
    }
}
