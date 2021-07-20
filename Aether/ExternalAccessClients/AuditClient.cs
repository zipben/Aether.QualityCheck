using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Models;
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
    public class AuditClient : IAuditClient
    {
        private const string AUDIT_CAPTURE_URL = "/audit";
        private readonly IHttpClientWrapper _httpClient;
        private readonly AuditClientConfig _config;

        public AuditClient(IHttpClientWrapper httpClient, IOptions<AuditClientConfig> config)
        {
            _httpClient     = Guard.Against.Null(httpClient, nameof(httpClient));
            _config         = Guard.Against.Null(config?.Value, nameof(ServiceConfig));

            ValidateConfig(config);

            _httpClient.SetBaseURI(config.Value.BaseUrl);
        }

        private static void ValidateConfig(IOptions<AuditClientConfig> config)
        {
            Guard.Against.NullOrWhiteSpace(config.Value.BaseUrl, nameof(config.Value.BaseUrl));
            Guard.Against.NullOrWhiteSpace(config.Value.Audience, nameof(config.Value.Audience));
            Guard.Against.NullOrWhiteSpace(config.Value.ClientID, nameof(config.Value.ClientID));
            Guard.Against.NullOrWhiteSpace(config.Value.ClientSecret, nameof(config.Value.ClientSecret));
        }

        public async Task CaptureAuditEvent(AuditEvent auditEvent)
        {
            try
            {
                var _auth0Auth = new Auth0AuthParams(_config.ClientID, _config.ClientSecret, _config.Audience);
                
                var response = await _httpClient.PostAsync(_auth0Auth, AUDIT_CAPTURE_URL, auditEvent.GenerateHttpStringContent());

                string responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message, e);
            }
        } 
    }
}
