using System.Collections.Generic;
using Aether.Enums;
using Aether.Interfaces;
using Newtonsoft.Json;

namespace Aether.Models
{
    public class EnforcementResponse : ModelBase, IEnforcementMessage
    {
        public string EnforcementRequestId { get; set; }

        /// The following is NOT set in enforcement responses for right to delete instead, these are classifications by-name 
        [JsonProperty("data")]
        public List<Classification> Data { get; set; } = new List<Classification>();
        public string RequestCloseReason { get; set; }
        public EnforcementType? EnforcementType { get; set; }

        public bool IsTestMessage
        {
            get
            {
                return base.DiagnosticFlags.Contains("IsTest");
            }
            set
            {
                if (value)
                    base.DiagnosticFlags.Add("IsTest");
                else
                    base.DiagnosticFlags.Remove("IsTest");
            }
        }
    }
}
