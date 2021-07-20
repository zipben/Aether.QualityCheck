﻿using System.Collections.Generic;
using Aether.Interfaces;

namespace ThemisBackend.Data.Configuration
{
    public abstract class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public bool DbInitialize { get; set; }
        public bool CreateIndexes { get; set; }
        public Dictionary<string, string> Indexes { get; set; }
    }
}
