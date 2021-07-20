using System;
using Aether.Interfaces.Themis;

namespace Aether.Models.Themis
{
    public class SystemRestriction : ISystemRestriction
    {
        public string Id { get; set; }
        public string SystemId { get; set; }
        public string SystemName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsRestricted { get; set; }
        public int CreatedById { get; set; }
        public int LastUpdatedById { get; set; }
    }
}
