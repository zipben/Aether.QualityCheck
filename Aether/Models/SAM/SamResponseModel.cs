using Aether.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.Sam
{
    public class SamResponseModel
    {
        public List<string> SourceValues { get; set; }
        public List<IIdentifier> CorrelatedIdentifiers { get; set; }
    }
}
