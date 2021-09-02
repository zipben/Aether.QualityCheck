using System;
using System.Collections.Generic;

namespace Aether.Models.Consent
{

    public class ConsentResponse
    {
        public Consentrecord[] consentRecords { get; set; }
        public bool overallConsent { get; set; }
    }

    public class Consentrecord
    {
        public string identifierMappingId { get; set; }
        public bool decision { get; set; }
        public bool _explicit { get; set; }
        public string consentCaptureType { get; set; }
        public string policyId { get; set; }
        public DateTime decisionDateTimeUtc { get; set; }
        public string createdBy { get; set; }
        public string clientIdentifierType { get; set; }
        public string clientIdentifier { get; set; }
    }

}
