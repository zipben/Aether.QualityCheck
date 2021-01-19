using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces
{
    public interface ISystemLookUp
    {
        string SystemName { get; set; }
        string EmailAddress { get; set; }
        string CreatedDate { get; set; }
        string LastUpdatedDate { get; set; }
    }
}
