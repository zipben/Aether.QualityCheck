using System.Collections.Generic;
using Aether.Interfaces.Configuration;

namespace Aether.Models.Configuration
{
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public bool DbInitialize { get; set; }
        public bool CreateIndexes { get; set; }
        public IEnumerable<MongoCollectionSettings> Collections { get; set; }
    }
}
