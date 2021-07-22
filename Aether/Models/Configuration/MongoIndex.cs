namespace Aether.Models.Configuration
{
    public class MongoIndex
    {
        public string Name { get; set; }
        public string Column { get; set; }
        public bool Unique { get; set; }
    }
}
