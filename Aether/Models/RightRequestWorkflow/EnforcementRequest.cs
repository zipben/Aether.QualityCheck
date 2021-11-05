using System;
using System.Collections.Generic;
using Aether.Enums;
using Aether.Interfaces;
using Aether.Interfaces.Oya;

namespace Aether.Models.RightRequestWorkflow
{
    public class EnforcementRequest : ModelBase, IEnforcementMessage
    {
        public string EnforcementRequestId { get; set; }

        public Dictionary<string, List<string>> Identifiers { get; set; }

        public EnforcementType? EnforcementType { get; set; }

        // Was DRE able to determine that QL has the SSN of the individual who made the request.
        // Default to true so that if DRE is not yet sending this field then we won't default to false
        public bool HasSSN { get; set; } = true;

        public DateTime? EnforcementRequestCreateDate { get; set; } 

        public bool IsTestMessage
        {
            get => base.DiagnosticFlags.Contains("IsTest");
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

