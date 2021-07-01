using System.Collections.Generic;
using Aether.Enums;

namespace Aether.Interfaces
{
    public interface IIdentifier
    {
        public IdentifierType IdentifierType { get; set; }
        public List<string> IdentifierValues { get; set; }
    }
}
