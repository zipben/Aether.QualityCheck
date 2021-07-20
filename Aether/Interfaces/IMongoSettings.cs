using System.Collections.Generic;

namespace Aether.Interfaces
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
        bool DbInitialize { get; set; }
        bool CreateIndexes { get; set; }
        Dictionary<string, string> Indexes { get; set; }
    }
}
