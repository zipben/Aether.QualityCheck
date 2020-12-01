using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aether.ExternalAccessClients.Interfaces;
using APILogger.Interfaces;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using RockLib.OAuth;

namespace Aether.ExternalAccessClients
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        private readonly IApiLogger _logger;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _policy;

        public HttpClientWrapper(HttpClient httpClient, IApiLogger logger)
        {
            _httpClient = httpClient;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _policy = HttpPolicyExtensions.HandleTransientHttpError()
                                          .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public void SetBaseURI(string endpoint) =>
            _httpClient.BaseAddress = new Uri(endpoint, UriKind.Absolute);

        public async Task<HttpResponseMessage> GetAsync(string requestUri) =>
            await _policy.ExecuteAsync(() => _httpClient.GetAsync(requestUri)).ConfigureAwait(false);

        public async Task<HttpResponseMessage> GetAsync(IAuthParams auth0Auth, string requestUri) =>
            await _policy.ExecuteAsync(() => _httpClient.GetAsync(auth0Auth, requestUri)).ConfigureAwait(false);

        public async Task<HttpResponseMessage> PatchAsync(IAuthParams authParams, string endPoint, HttpContent content)
        {
            using var request = new HttpRequestMessage(HttpMethod.Patch, endPoint) { Content = content };
            return await _httpClient.SendAsync(authParams, request);
        }

        public async Task<HttpResponseMessage> PostAsync(IAuthParams authParams, string endPoint, HttpContent content) =>
            await _httpClient.PostAsync(authParams, endPoint, content);

        public async Task<HttpResponseMessage> PutAsync(IAuthParams authParams, string endPoint, HttpContent content) =>
            await _httpClient.PutAsync(authParams, endPoint, content);

        public void SetContentType(string contentType) =>
            _httpClient.DefaultRequestHeaders
                       .Accept
                       .Add(new MediaTypeWithQualityHeaderValue(contentType));
    }
}
