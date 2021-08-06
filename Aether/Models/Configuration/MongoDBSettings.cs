using System.Collections.Generic;
using Aether.Interfaces.Configuration;

namespace Aether.Models.Configuration
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public bool DbInitialize { get; set; }
        public IEnumerable<MongoDBCollectionSettings> Collections { get; set; }
    }
}
