using System.Collections.Generic;

namespace Aether.Models.Consent
{
    public class ConsentResponse
    {
        public string Version { get; set; }
        public Identifier Identifier { get; set; }
        public List<PolicyDecision> PolicyDecisions { get; set; }

    }

    public class Identifier
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class PolicyDecision
    {

        public bool Decision { get; set; }
        public bool Explicit { get; set; }
        public string Policy { get; set; }
        public string CaptureType { get; set; }
        public string DecisionDateUTC { get; set; }
        public string CreatedBy { get; set; }
    }
}
