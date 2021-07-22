using System.Collections.Generic;

namespace Aether.Models.Configuration
{
    public class MongoDBCollectionSettings
    {
        public string Name { get; set; }
        public bool CreateIndexes { get; set; }
        public IEnumerable<MongoDBIndexSettings> Indexes { get; set; }
    }
}
