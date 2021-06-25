using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.ErisClient
{
    public class CorrelatedIdentifierResponseModel
    {
        public string SourceType { get; set; }
        public string SourceValue { get; set; }
        public bool isResolvedSuccessfully { get; set; }
        public string errorMessage { get; set; }
        public Dictionary<string, List<string>> CorrelatedIdentifiers { get; set; }

        public CorrelatedIdentifierResponseModel()
        {
            this.CorrelatedIdentifiers = new Dictionary<string, List<string>>();
            this.isResolvedSuccessfully = true;
        }
    }
}
