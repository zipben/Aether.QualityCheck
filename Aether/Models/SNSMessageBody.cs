using System;
using Newtonsoft.Json;

namespace Aether.Models
{
    public class MessageBody
    {
        [JsonProperty("Type")]
        public string MessageType { get; set; }

        public string MessageId { get; set; }
        public string TopicArn { get; set; }
        [JsonProperty("Message")]
        public string SerializedMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public int SignatureVersion { get; set; }
        public string Signature { get; set; }
        public string SigningCertUrl { get; set; }
        public string UnsubscribeUrl { get; set; }
    }
}
