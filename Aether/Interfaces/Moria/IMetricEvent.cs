using System;
using System.Net.Http;

namespace Aether.Interfaces.Moria
{
    public interface IMetricEvent
    {
        public string CalledBy { get; set; }
        public DateTime CalledDate { get; set; }
        public string CalledFrom { get; set; }
        public HttpMethod Method { get; set; }
        public string Url { get; set; }
    }
}
