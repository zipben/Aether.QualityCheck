using System.Collections.Generic;
using Aether.Enums;
using Aether.Interfaces;

namespace Aether.Models
{
    public class Identifier: IIdentifier
    {
        public IdentifierType IdentifierType { get; set; }
        public List<string> IdentifierValues { get; set; }

        public Identifier()
        {
            IdentifierValues = new List<string>();
        }
    }
}
