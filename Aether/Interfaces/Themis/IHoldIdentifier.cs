using System;
using Aether.Enums;

namespace Aether.Interfaces.Themis
{
    public interface IHoldIdentifier
    {
        public string Id { get; set; }
        public string HoldName { get; set; }
        public IdentifierType IdentifierType { get; set; }
        public string Value { get; set; }
        public ResolutionStatus ResolutionStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
