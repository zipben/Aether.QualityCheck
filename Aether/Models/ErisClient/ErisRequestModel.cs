using Aether.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.ErisClient
{
    public class ErisRequestModel
    {
        public IdentifierType Source { get; set; }
        public IdentifierType Destination { get; set; }
        public List<string> Identifiers { get; set; }
    }
}
