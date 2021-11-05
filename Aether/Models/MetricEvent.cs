using System;
using System.Net.Http;
using Aether.Interfaces.Moria;

namespace Aether.Models
{
    public class MetricEvent : IMetricEvent
    {
        public string CalledBy { get; set; }
        public DateTime CalledDate { get; set; }
        public string CalledFrom { get; set; }
        public HttpMethod Method { get; set; }
        public string Url { get; set; }
    }
}
