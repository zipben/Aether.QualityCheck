using Aether.Enums;
using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Interfaces.ExternalAccessClients;
using Aether.Models.Configurations;
using Aether.Models.Consent;
using Ardalis.GuardClauses;
using Juno.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RockLib.OAuth;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using Aether.Models.Configuration;
using Aether.Extensions.Models.Configuration;

namespace Aether.ExternalAccessClients
{
    public class ConsentClient : IConsentClient
    {
        private readonly ConsentConfiguration _config;
        private readonly ServiceOAuthConfiguration _clientPlatformSettings;
        private readonly IHttpClientWrapper _httpClient;

        private const string URL_PATH = "api/consent/";

        public ConsentClient(IHttpClientWrapper httpClient, IOptionsMonitor<ServiceOAuthConfiguration> serviceOAuthConfiguration)
        {
            _clientPlatformSettings = Guard.Against.Null(serviceOAuthConfiguration?.Get(Constants.Consent.CONSENT_SETTINGS), nameof(serviceOAuthConfiguration));

            Guard.Against.Null(_clientPlatformSettings.Audience,        nameof(_clientPlatformSettings.Audience));
            Guard.Against.Null(_clientPlatformSettings.AuthorizerUrl,   nameof(_clientPlatformSettings.AuthorizerUrl));
            Guard.Against.Null(_clientPlatformSettings.BaseUrl,         nameof(_clientPlatformSettings.BaseUrl));
            Guard.Against.Null(_clientPlatformSettings.ClientId,        nameof(_clientPlatformSettings.ClientId));
            Guard.Against.Null(_clientPlatformSettings.ClientSecret,    nameof(_clientPlatformSettings.ClientSecret));

            _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));

            _httpClient.SetBaseURI(_clientPlatformSettings.BaseUrl);
        }

        public async Task<ConsentResponse> GetBatchConsentFromDps(IdentifierType clientIdentifierType, List<string> identifiers)
        {
            var endpointArgs = $"{URL_PATH}clients";
            HttpContent batchRequests = CreateBatchsAsync(clientIdentifierType, identifiers);
            var consentDreResponse = await _httpClient.PostAsync(_clientPlatformSettings.GetAuthParams(), endpointArgs, batchRequests);

            Guard.Against.UnsuccessfulHttpRequest(consentDreResponse);

            var message = await consentDreResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ConsentResponse>(message);

        }

        private static HttpContent CreateBatchsAsync(IdentifierType clientIdentifierType, List<string> identifiers)
        {
            List<ConsentBatchRequest> batch = new List<ConsentBatchRequest>();
            foreach (var id in identifiers)
            {
                var batchRequest = new ConsentBatchRequest
                {
                    ClientIdentifierType = clientIdentifierType.ToString(),
                    ClientIdentifier = id
                };
                batch.Add(batchRequest);
            }
            var batchRequests = batch.GenerateHttpStringContent();
            return batchRequests;
        }
    }
}
