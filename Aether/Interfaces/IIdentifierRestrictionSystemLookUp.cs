using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces
{
    public interface IIdentifierRestrictionSystemLookUp
    {
        public string Id { get; set; }
        public string SystemId { get; set; }
        public string SystemName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
 