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

namespace Aether.ExternalAccessClients
{
    public class ConsentClient : IConsentClient
    {
        private readonly ConsentConfiguration _config;
        private readonly IHttpClientWrapper _httpClient;

        private const string URL_PATH = "api/consent/";

        public ConsentClient(IHttpClientWrapper httpClient, IOptions<ConsentConfiguration> clientConfiguration)
        {
            var clientConfig    = Guard.Against.Null(clientConfiguration, nameof(clientConfiguration));
            _config             = Guard.Against.Null(clientConfig?.Value, nameof(clientConfig.Value));

            _httpClient         = Guard.Against.Null(httpClient, nameof(httpClient));

            _httpClient.SetBaseURI(_config.BaseUrl);
        }

        public async Task<ConsentResponse> GetSingleConsentFromDps(IdentifierType clientIdentifierType, string identifier)
        {
            var endpointArgs = $"{URL_PATH}{clientIdentifierType}/{identifier}";
            _httpClient.AddDefaultRequestHeader("X-Version", "2");
            var consentDreResponse = await _httpClient.GetAsync(GenerateAuthParam(), endpointArgs);

            Guard.Against.UnsuccessfulHttpRequest(consentDreResponse);

            var message = await consentDreResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ConsentResponse>(message);

        }
        public async Task<ConsentResponse> GetBatchConsentFromDps(IdentifierType clientIdentifierType, List<string> identifiers)
        {
            var endpointArgs = $"{URL_PATH}clients";
            HttpContent batchRequests = CreateBatchsAsync(clientIdentifierType, identifiers);
            var consentDreResponse = await _httpClient.PostAsync(GenerateAuthParam(), endpointArgs, batchRequests);

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

        protected IAuthParams GenerateAuthParam() =>
            new Auth0AuthParams(_config.ClientID, _config.ClientSecret, _config.Audience);
    }
}
