using Aether.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.Themis
{
    public class Identifier
    {
        public IdentifierType IdentifierType { get; set; }
        public List<string> IdentifierValues { get; set; }

        public Identifier()
        {
            IdentifierValues = new List<string>();
        }
    }
}
