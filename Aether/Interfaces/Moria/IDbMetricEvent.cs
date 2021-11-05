namespace Aether.Interfaces.Moria
{
    public interface IDbMetricEvent
    {
        public string Endpoint { get; set; }
        public string Called { get; set; }
    }
}
