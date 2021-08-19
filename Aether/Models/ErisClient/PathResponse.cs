using Aether.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.ErisClient
{
    public class PathResponse
    {
        public List<Path> Paths { get; set; }
    }

    public class Path
    {
        public IdentifierType Source { get; set; }
        public IdentifierType Destination { get; set; }
    }
}
