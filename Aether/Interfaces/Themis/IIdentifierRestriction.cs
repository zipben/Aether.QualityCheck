using Aether.Enums;

namespace Aether.Interfaces.Themis
{
    public interface IIdentifierRestriction : IRestriction
    {
        public IdentifierType IdentifierType { get; set; }
    }
}