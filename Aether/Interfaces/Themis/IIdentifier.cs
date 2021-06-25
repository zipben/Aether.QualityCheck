using Aether.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces.Themis
{
    public interface IIdentifier
    {
        public IdentifierType IdentifierType { get; set; }
        public List<string> IdentifierValues { get; set; }
    }
}
