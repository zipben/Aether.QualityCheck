using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public class CreditMetadata
    {
        public string ScrubbedDataUrl { get; set; }
        public string CreditGuid { get; set; }
        public string RuquestType { get; set; }
        public string ActionType { get; set; }
        public string LoanNumber { get; set; }
        public string PullType { get; set; }
        public bool IsSinglueBureau { get; set; }
        public bool IsTriMerge { get; set; }
        public bool IsOnTheFlyUpgrade { get; set; }
        public bool IsDuplicateFromDb { get; set; }
        public bool IsTestCase { get; set; }
    }
}
