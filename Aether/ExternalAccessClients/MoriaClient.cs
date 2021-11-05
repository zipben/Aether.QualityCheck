using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Interfaces.Moria;
using Aether.Models.Configuration;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using RockLib.OAuth;

namespace Aether.ExternalAccessClients
{
    public class MoriaClient : IMoriaClient
    {
        private const string AUDIT_CAPTURE_URL = "/audit";
        private const string METRIC_CAPTURE_URL = "/audit";
        private readonly IHttpClientWrapper _httpClient;
        private readonly MoriaClientConfig _config;

        public MoriaClient(IHttpClientWrapper httpClient, IOptions<MoriaClientConfig> config)
        {
            _httpClient =   Guard.Against.Null(httpClient, nameof(httpClient));
            _config =       Guard.Against.Null(config?.Value, nameof(ServiceConfig));

            ValidateConfig(config);

            _httpClient.SetBaseURI(config.Value.BaseUrl);
        }

        private static void ValidateConfig(IOptions<MoriaClientConfig> config)
        {
            Guard.Against.NullOrWhiteSpace(config.Value.BaseUrl, nameof(config.Value.BaseUrl));
            Guard.Against.NullOrWhiteSpace(config.Value.Audience, nameof(config.Value.Audience));
            Guard.Against.NullOrWhiteSpace(config.Value.ClientID, nameof(config.Value.ClientID));
            Guard.Against.NullOrWhiteSpace(config.Value.ClientSecret, nameof(config.Value.ClientSecret));
        }

        public async Task CaptureAuditEvent(IAuditEvent auditEvent)
        {
            try
            {
                var response = await _httpClient.PostAsync(GetAuth0AuthParams(), AUDIT_CAPTURE_URL, auditEvent.GenerateHttpStringContent());

                string responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message, e);
            }
        }

        public async Task CaptureMetricEvent(IMetricEvent metricEvent)
        {
            try
            {
                var response = await _httpClient.PostAsync(GetAuth0AuthParams(), METRIC_CAPTURE_URL, metricEvent.GenerateHttpStringContent());

                string responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message, e);
            }
        }

        private Auth0AuthParams GetAuth0AuthParams() => new Auth0AuthParams(_config.ClientID, _config.ClientSecret, _config.Audience);
    }
}
