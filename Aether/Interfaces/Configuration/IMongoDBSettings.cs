﻿using System.Collections.Generic;
using Aether.Models.Configuration;

namespace Aether.Interfaces.Configuration
{
    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        bool DbInitialize { get; set; }
        public IEnumerable<MongoDBCollectionSettings> Collections { get; set; }
    }
}