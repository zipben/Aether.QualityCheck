using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aether.Models.Kafka
{
    public class KafkaIdentifier
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public List<string> Value { get; set; }
    }
}
