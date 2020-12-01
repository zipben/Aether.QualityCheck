using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aether.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aether.ExternalAccessClients
{
    public abstract class HttpClientProvider
    {
        private const string ACCESS_TOKEN = "access_token";
        protected readonly HttpClient _httpClient;
        private static readonly List<CachedToken> _tokenCache = new List<CachedToken>();

        protected HttpClientProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<string> GetToken(ServiceSettings serviceSettings, string scope)
        {
            var cachedToken = GetTokenFromCache(serviceSettings, scope);
            if (!string.IsNullOrWhiteSpace(cachedToken))
            {
                return cachedToken;
            }

            using var httpResponse = await GetHttpResponse(serviceSettings, scope);

            var token = await GetTokenFromHttpResponse(httpResponse);

            if (!string.IsNullOrWhiteSpace(token))
            {
                AddToCache(token, serviceSettings, scope);
            }

            return token;
        }

        private async Task<HttpResponseMessage> GetHttpResponse(ServiceSettings serviceSettings, string scope)
        {
            using var httpContent = GetHttpRequestContent(serviceSettings, scope);
            using var httpRequestMessage = CreateHttpRequestMessage(
                GetAuthorizeUrl(serviceSettings, scope),
                HttpMethod.Post,
                GetHeaders(),
                GetAuthenticationHeader(serviceSettings),
                httpContent
            );

            return await _httpClient.SendAsync(httpRequestMessage);
        }

        protected HttpRequestMessage CreateHttpRequestMessage(string url, HttpMethod method, List<KeyValuePair<string, string>> headers, AuthenticationHeaderValue auth, HttpContent content) =>
            CreateHttpRequestMessage(new Uri(url), method, headers, auth, content);

        protected HttpRequestMessage CreateHttpRequestMessage(Uri uri, HttpMethod method, List<KeyValuePair<string, string>> headers, AuthenticationHeaderValue auth, HttpContent content)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = method
            };

            if (headers != null)
            {
                headers.ForEach(h => request.Headers.Add(h.Key, h.Value));
            }

            if (auth != null)
            {
                request.Headers.Authorization = auth;
            }

            if (content != null)
            {
                request.Content = content;
            }

            return request;
        }

        private string GetAuthorizeUrl(ServiceSettings serviceSettings, string scope)
        {
            var url = serviceSettings.AuthorizerUrl;

            if (serviceSettings.SendParametersInQueryString)
            {
                var parameters = GetParameters(serviceSettings, scope);
                foreach (var parameter in parameters)
                {
                    url = $"{url}{GetParameterToken(url)}{parameter.Key}={parameter.Value}";
                }
            }

            return url;
        }

        private List<KeyValuePair<string, string>> GetHeaders() =>
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Accept","application/json")
            };

        private AuthenticationHeaderValue GetAuthenticationHeader(ServiceSettings serviceSettings)
        {
            if (serviceSettings.UseBasicAuthentication)
            {
                var byteArray = Encoding.ASCII.GetBytes($"{serviceSettings.UserName}:{serviceSettings.Password}");
                return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }
            else
            {
                return null;
            }
        }

        private HttpContent GetHttpRequestContent(ServiceSettings serviceSettings, string scope)
        {
            if (serviceSettings.SendParametersInQueryString)
            {
                return null;
            }
            else
            {
                var parameters = GetParameters(serviceSettings, scope);
                var requestContent = new FormUrlEncodedContent(parameters);
                return new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestContent)));
            }
        }

        private List<KeyValuePair<string, string>> GetParameters(ServiceSettings serviceSettings, string scope)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", serviceSettings.GrantType.ToString().ToLower()),
            };
            if (!string.IsNullOrEmpty(serviceSettings.BaseUrl))
            {
                parameters.Add(new KeyValuePair<string, string>("aud", serviceSettings.BaseUrl));
            }
            if (!string.IsNullOrEmpty(scope))
            {
                parameters.Add(new KeyValuePair<string, string>("scope", scope));
            }
            if (!serviceSettings.UseBasicAuthentication)
            {
                parameters.Add(new KeyValuePair<string, string>("username", serviceSettings.UserName));
                parameters.Add(new KeyValuePair<string, string>("password", serviceSettings.Password));
            }
            return parameters;
        }

        private char GetParameterToken(string url) => url.Contains('?') ? '&' : '?';

        private async Task<string> GetTokenFromHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var json = (JObject)JsonConvert.DeserializeObject(content);
            return json[ACCESS_TOKEN].Value<string>();
        }

        private string GetTokenFromCache(ServiceSettings serviceSettings, string scope)
        {
            RemoveExpiredTokens();

            var cachedToken = _tokenCache.FirstOrDefault(x => !string.IsNullOrEmpty(x.Token)
                && x.AuthorizationUrl == serviceSettings.AuthorizerUrl
                && x.User == serviceSettings.UserName
                && x.Aud == serviceSettings.BaseUrl
                && x.Scope == scope
                && DateTime.Now.Subtract(x.DateCached).TotalMinutes <= 1);

            return cachedToken?.Token;
        }

        private void AddToCache(string token, ServiceSettings serviceSettings, string scope)
        {
            RemoveExpiredTokens();

            _tokenCache.Add(new CachedToken
            {
                Aud = serviceSettings.BaseUrl,
                AuthorizationUrl = serviceSettings.AuthorizerUrl,
                DateCached = DateTime.Now,
                Scope = scope,
                Token = token,
                User = serviceSettings.UserName
            });
        }

        private void RemoveExpiredTokens()
        {
            for (var i = _tokenCache.Count - 1; i >= 0; i--)
            {
                var token = _tokenCache[i];
                if (DateTime.Now.Subtract(token.DateCached).TotalMinutes > 1)
                {
                    _tokenCache.RemoveAt(i);
                }
            }
        }
    }
}
