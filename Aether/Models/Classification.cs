using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aether.Models
{
    public class Classification
    {
        [JsonProperty("classificationName")]
        public string ClassificationName { get; set; }

        [JsonProperty("fieldValues")]
        public Dictionary<string, List<string>> FieldValues { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }
}
