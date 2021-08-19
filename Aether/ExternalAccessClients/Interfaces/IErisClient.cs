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
        Task<List<Path>> GetAllPaths();
        Task<List<Path>> GetAllTestPaths();
    }
}
