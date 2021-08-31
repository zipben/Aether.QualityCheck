using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Models.Configuration;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using RockLib.OAuth;
using RockParty.NETCore.Helpers;
using System.Threading.Tasks;

namespace Aether.ExternalAccessClients

{
    public class CreditV2Client : ICreditV2Client
    {
        public readonly IHttpClientWrapper _httpClient;
        CreditV2Configuration _config;

        public CreditV2Client(IHttpClientWrapper httpClient, IOptions<CreditV2Configuration> clientConfiguration)
        {
            _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));

            var clientConfig = Guard.Against.Null(clientConfiguration, nameof(clientConfiguration));

            _config = Guard.Against.Null(clientConfig.Value, nameof(clientConfig.Value));

            Guard.Against.NullOrEmpty(_config.ClientSecret, nameof(_config.ClientSecret));
            Guard.Against.NullOrEmpty(_config.ClientID, nameof(_config.ClientID));
            Guard.Against.NullOrEmpty(_config.BaseUrl, nameof(_config.BaseUrl));
        }

        public async Task<Credit.Mismo.RESPONSE_GROUP> PullCredit(string creditGuid)
        {
            Guard.Against.NullOrEmpty(creditGuid, nameof(creditGuid));

            var endpoint = $"{_config.BaseUrl}/XbertXmlData?reportType=VendorResponse&GUID={creditGuid}";

            _httpClient.AddDefaultRequestHeader("client_Id", _config.ClientID);
            _httpClient.AddDefaultRequestHeader("client_secret", _config.ClientSecret);

            var creditResponse = await _httpClient.GetAsync(endpoint);
            var content = await creditResponse.Content.ReadAsStringAsync();
            var creditReport = XmlHelper.DeserializeFromXmlString<Credit.Mismo.RESPONSE_GROUP>(content);
            return creditReport;
        }
    }
}
