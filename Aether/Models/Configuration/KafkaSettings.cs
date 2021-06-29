namespace Aether.Models.Configuration
{
    public class KafkaSettings
    {
        public string HostName { get; set; }
        public string Password { get; set; }
        public bool SendMessages { get; set; }
        public string UserName { get; set; }
    }
}
