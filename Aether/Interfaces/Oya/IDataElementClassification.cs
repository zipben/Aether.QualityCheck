using System.Collections.Generic;

namespace Aether.Interfaces
{
    public interface IDataElementClassification
    {
        public string Entity { get; set; }
        public string Classification { get; set; }
        //this key matches personal data in ledgerEntry
        public Dictionary<string, string> DataElements { get; set; }
        public bool HasSSN { get; set; }
        public string Category { get; set; }
        public string MaskedValue { get; set; }
        public string ReplaceValue { get; set; }
        public string DecryptionKey { get; set; }
    }
}
