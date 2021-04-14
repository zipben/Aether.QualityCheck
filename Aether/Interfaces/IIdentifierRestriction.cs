using System;
using Aether.Enums;

namespace Aether.Interfaces
{
    public interface IIdentifierRestriction
    {
        public string Id { get; set; }
        public IdentifierType IdentifierType { get; set; }
        public bool IsRestricted { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
