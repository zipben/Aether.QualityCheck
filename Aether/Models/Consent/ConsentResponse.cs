using System;
using System.Collections.Generic;

namespace Aether.Models.Consent
{

    public class ConsentResponse
    {
        public ConsentRecord[] ConsentRecords { get; set; }
        public bool OverallConsent { get; set; }
    }

    public class ConsentRecord
    {
        public string IdentifierMappingId { get; set; }
        public bool Decision { get; set; }
        public bool _Explicit { get; set; }
        public string ConsentCaptureType { get; set; }
        public string PolicyId { get; set; }
        public DateTime DecisionDateTimeUtc { get; set; }
        public string CreatedBy { get; set; }
        public string ClientIdentifierType { get; set; }
        public string ClientIdentifier { get; set; }
    }

}
