using Aether.Enums;
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
        Task<IdentifiersRoot> ResolveTestIdentifiersAsync(IdentifierRequestModel erisRequestModel);
        Task<List<(IdentifierType source, IdentifierType destination)>> GetAllPaths();
        Task<List<(IdentifierType source, IdentifierType destination)>> GetAllTestPaths();
    }
}
