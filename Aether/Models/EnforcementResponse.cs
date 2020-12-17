using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aether.Models
{
    public class EnforcementResponse
    {
        public string EnforcementRequestId { get; set; }

        /// The following is NOT set in enforcement responses for right to delete instead, these are classifications by-name 
        [JsonProperty("data")]
        public List<Classification> Data { get; set; } = new List<Classification>();

        // Indicates we patch to the test endpoint
        public bool IsTestMessage { get; set; }
    }
}
