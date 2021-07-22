using System.Collections.Generic;

namespace Aether.Models.Configuration
{
    public class MongoCollectionSettings
    {
        public string Name { get; set; }
        public IEnumerable<MongoIndexSettings> Indexes { get; set; }
    }
}
