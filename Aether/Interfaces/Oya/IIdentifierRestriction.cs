using Aether.Enums;

namespace Aether.Interfaces.Oya
{
    public interface IIdentifierRestriction : IRestriction
    {
        public IdentifierType IdentifierType { get; set; }
    }
}