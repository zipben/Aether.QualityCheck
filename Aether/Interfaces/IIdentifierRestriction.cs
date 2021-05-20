using Aether.Enums;

namespace Aether.Interfaces
{
    public interface IIdentifierRestriction : IRestriction
    {
        public IdentifierType IdentifierType { get; set; }
    }
}