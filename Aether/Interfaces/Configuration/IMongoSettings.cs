using System.Collections.Generic;
using Aether.Models.Configuration;

namespace Aether.Interfaces.Configuration
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        bool DbInitialize { get; set; }
        bool CreateIndexes { get; set; }
        public IEnumerable<MongoCollectionSettings> Collections { get; set; }
    }
}
