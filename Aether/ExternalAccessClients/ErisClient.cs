using Aether.ExternalAccessClients.Interfaces;
using Aether.Models.ErisClient;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RockLib.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aether.ExternalAccessClients
{
    public class ErisClient : IErisClient
    {
        private const string ERIS_RESOLVER_URL = "/api/identifier/resolver";
        private readonly IHttpClientWrapper _httpClient;
        private readonly ErisConfig _config;

        public ErisClient(IHttpClientWrapper httpClient, IOptions<ErisConfig> config)
        {
            _httpClient =   Guard.Against.Null(httpClient, nameof(httpClient));
            _config =       Guard.Against.Null(config?.Value, nameof(ErisConfig));

            ValidateConfig(config);

            _httpClient.SetBaseURI(config.Value.BaseUrl);
        }

        private static void ValidateConfig(IOptions<ErisConfig> config)
        {
            Guard.Against.NullOrWhiteSpace(config.Value.Audience, nameof(config.Value.Audience));
            Guard.Against.NullOrWhiteSpace(config.Value.BaseUrl, nameof(config.Value.BaseUrl));
            Guard.Against.NullOrWhiteSpace(config.Value.ClientID, nameof(config.Value.ClientID));
            Guard.Against.NullOrWhiteSpace(config.Value.ClientSecret, nameof(config.Value.ClientSecret));
        }

        public async Task<IdentifiersRoot> ResolveIdentifiersAsync(IdentifierRequestModel erisRequestModel)
        {
            Guard.Against.Null(erisRequestModel, nameof(erisRequestModel));

            using StringContent stringContent = GenerateRequestBody(erisRequestModel);

            return await RetrieveIdentifiers(stringContent).ConfigureAwait(false);
        }

        private async Task<IdentifiersRoot> RetrieveIdentifiers(StringContent content)
        {
            content.Headers.ContentType.MediaType = "application/json";
            using HttpResponseMessage httpResponseMessage = await TryPostRequestAsync(content);

            ValidateHttpResponse(httpResponseMessage);

            var serializedContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IdentifiersRoot>(serializedContent);
        }

        private static void ValidateHttpResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Reason: {response.ReasonPhrase}, Status Code: {response.StatusCode}");

            if (response.Content == null)
                throw new HttpRequestException($"Null Content, Status Code: {response.StatusCode}");
        }

        private async Task<HttpResponseMessage> TryPostRequestAsync(StringContent content)
        {
            try
            {
                var _auth0Auth = new Auth0AuthParams(_config.ClientID, _config.ClientSecret, _config.Audience);
                return await _httpClient.PostAsync(_auth0Auth, ERIS_RESOLVER_URL, content);
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message, e);
            }
        }

        private StringContent GenerateRequestBody(IdentifierRequestModel erisRequestModel)
        {
            try
            {
                return new StringContent(JsonConvert.SerializeObject(erisRequestModel));
            }
            catch (Exception e)
            {
                throw new JsonSerializationException("Encountered an exception while trying to serialize request model into string content", e);
            }
        }
    }
}
