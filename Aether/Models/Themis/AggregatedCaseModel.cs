using System;
using System.Collections.Generic;

namespace Aether.Models.Themis
{
    public class AggregatedCaseModel
    {
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DateHoldCreated { get; set; }
        public DateTime DateHoldEnded { get; set; }
        public string Id { get; set; }
        public HashSet<string> Gcids { get; set; }
        public HashSet<string> LoanNumbers { get; set; }
        public HashSet<string> RMClientID { get; set; }
        public HashSet<string> RMLoanId { get; set; }

        public AggregatedCaseModel()
        {
            Gcids = new HashSet<string>();
            LoanNumbers = new HashSet<string>();
            RMClientID = new HashSet<string>();
            RMLoanId = new HashSet<string>();
        }
    }
}
