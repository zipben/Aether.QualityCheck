using Aether.Enums;
using Aether.Models.Consent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aether.Interfaces.ExternalAccessClients
{
    public interface IConsentClient
    {
        Task<ConsentResponse> GetBatchConsentFromDps(IdentifierType clientIdentifier, List<string> identifiers);
        Task<ConsentResponse> GetSingleConsentFromDps(IdentifierType clientIdentifier, string identifier);
    }
}