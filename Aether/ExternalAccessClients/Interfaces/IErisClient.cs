using Aether.Models.ErisClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface IErisClient
    {
        Task<IdentifiersRoot> ResolveIdentifiersAsync(IdentifierRequestModel erisRequestModel);
    }
}
