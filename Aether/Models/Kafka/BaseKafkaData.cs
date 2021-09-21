using Newtonsoft.Json;

namespace Aether.Models.Kafka
{
    public abstract class BaseKafkaData
    {
        [JsonProperty("dataschema")]
        public string DataSchema { get; set; }
    }
}
