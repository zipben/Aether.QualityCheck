using System.Collections.Generic;

namespace Aether.Models.Themis
{
    public class LitigationResponseWarning
    {
        public string CaseName { get; set; }
        public List<string> Messages { get; set; }
    }
}
