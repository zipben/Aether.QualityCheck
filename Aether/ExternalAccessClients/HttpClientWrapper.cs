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

        public HttpClientWrapper(HttpClient httpClient, IApiLogger logger = null)
        {
            _httpClient = httpClient;
            if (logger != null)
            {
                _logger = logger;
            }
            _policy = HttpPolicyExtensions.HandleTransientHttpError()
                                          .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public Uri GetBaseURI() =>
            _httpClient.BaseAddress;

        public void SetBaseURI(string endpoint) =>
            _httpClient.BaseAddress = new Uri(endpoint, UriKind.Absolute);

        public async Task<HttpResponseMessage> DeleteAsync(IAuthParams authParams, string endPoint, HttpContent content) => 
            await DeleteAsync(authParams, endPoint, content, null); 

        public async Task<HttpResponseMessage> DeleteAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, endPoint) { Content = content };

            if (callInitiator != null)
                request.Headers.Add(Constants.CALL_INITIATOR_HEADER_KEY, callInitiator);

            return await _httpClient.SendAsync(authParams, request);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri) =>
            await _policy.ExecuteAsync(() => _httpClient.GetAsync(requestUri)).ConfigureAwait(false);


        public async Task<HttpResponseMessage> GetAsync(IAuthParams auth0Auth, string requestUri) => 
            await GetAsync(auth0Auth, requestUri, null);
            
        public async Task<HttpResponseMessage> GetAsync(IAuthParams auth0Auth, string requestUri, string callInitiator = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            if (callInitiator != null)
                request.Headers.Add(Constants.CALL_INITIATOR_HEADER_KEY, callInitiator);

            return await _policy.ExecuteAsync(() => _httpClient.SendAsync(auth0Auth, request)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchAsync(IAuthParams authParams, string endPoint, HttpContent content) =>
            await PatchAsync(authParams, endPoint, content, null);

        public async Task<HttpResponseMessage> PatchAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Patch, endPoint) { Content = content };

            if (callInitiator != null)
                request.Headers.Add(Constants.CALL_INITIATOR_HEADER_KEY, callInitiator);

            return await _httpClient.SendAsync(authParams, request);
        }

        public async Task<HttpResponseMessage> PostAsync(IAuthParams authParams, string endPoint, HttpContent content) =>
            await PostAsync(authParams, endPoint, content, null);

        public async Task<HttpResponseMessage> PostAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, endPoint) { Content = content };

            if (callInitiator != null)
                request.Headers.Add(Constants.CALL_INITIATOR_HEADER_KEY, callInitiator);

            return await _httpClient.SendAsync(authParams, request);
        }

        public async Task<HttpResponseMessage> PutAsync(IAuthParams authParams, string endPoint, HttpContent content) =>
            await PutAsync(authParams, endPoint, content, null);

        public async Task<HttpResponseMessage> PutAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, endPoint) { Content = content };

            if (callInitiator != null)
                request.Headers.Add(Constants.CALL_INITIATOR_HEADER_KEY, callInitiator);

            return await _httpClient.SendAsync(authParams, request);
        }

        public void SetContentType(string contentType) =>
            _httpClient.DefaultRequestHeaders
                       .Accept
                       .Add(new MediaTypeWithQualityHeaderValue(contentType));

        public void AddDefaultRequestHeader(string key, string value) =>
             _httpClient.DefaultRequestHeaders.Add(key, value);
    }
}
