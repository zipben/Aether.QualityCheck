using System;
using Aether.Enums;
using Aether.Interfaces.Themis;

namespace Aether.Models.Themis
{
    public class IdentifierRestriction : IIdentifierRestriction
    {
        public IdentifierType IdentifierType { get; set; }
        public bool IsRestricted { get; set; }
        public string Id { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
