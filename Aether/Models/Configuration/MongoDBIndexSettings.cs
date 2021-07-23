namespace Aether.Models.Configuration
{
    public class MongoDBIndexSettings
    {
        public string Name { get; set; }
        public string Column { get; set; }
        public bool? Unique { get; set; } = null;
    }
}
