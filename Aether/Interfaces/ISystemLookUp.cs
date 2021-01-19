using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces
{
    public interface ISystemLookup
    {
        string SystemName { get; set; }
        string EmailAddress { get; set; }
        long CreatedDate { get; set; }
        long LastUpdatedDate { get; set; }
    }
}
