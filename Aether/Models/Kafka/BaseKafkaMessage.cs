using System;
using Newtonsoft.Json;

namespace Aether.Models.Kafka
{
    public abstract class BaseKafkaMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("datacontenttype")]
        public string DataContentType { get; set; }

        [JsonProperty("dataschema")]
        public string DataSchema { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("specversion")]
        public string SpecVersion { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }
}
