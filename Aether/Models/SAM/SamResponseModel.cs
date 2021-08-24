using Aether.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.Sam
{
    public class SamResponseModel
    {
        public string RequestId {get; set;}
        public List<string> SourceValues { get; set; }
        public List<Identifier> CorrelatedIdentifiers { get; set; }
    }
}
