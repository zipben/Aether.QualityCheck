using Newtonsoft.Json;

namespace Aether.Models.Kafka
{
    public abstract class BaseKafkaData
    {
        [JsonProperty("datacontenttype")]
        public string DataContentType { get; set; }

        [JsonProperty("dataschema")]
        public string DataSchema { get; set; }
    }
}
