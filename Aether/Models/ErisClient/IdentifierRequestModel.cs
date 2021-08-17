using Aether.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.ErisClient
{
    /// <summary>
    /// Used for requests sent to both SAM, and Eris
    /// </summary>
    public class IdentifierRequestModel
    {
        public IdentifierType Source { get; set; }
        public IdentifierType Destination { get; set; }
        public List<string> Identifiers { get; set; }
    }
}
