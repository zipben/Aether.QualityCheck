using System;
using System.Net.Http;
using System.Threading.Tasks;
using RockLib.OAuth;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface IHttpClientWrapper
    {
        Uri GetBaseURI();
        void SetBaseURI(string endpoint);
        Task<HttpResponseMessage> DeleteAsync(IAuthParams authParams, string endPoint, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null);
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> GetAsync(IAuthParams auth0Auth, string requestUri);
        Task<HttpResponseMessage> GetAsync(IAuthParams auth0Auth, string requestUri, string callInitiator = null);
        Task<HttpResponseMessage> PatchAsync(IAuthParams authParams, string endPoint, HttpContent content);
        Task<HttpResponseMessage> PatchAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null);
        Task<HttpResponseMessage> PostAsync(IAuthParams authParams, string endPoint, HttpContent content);
        Task<HttpResponseMessage> PostAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null);
        Task<HttpResponseMessage> PutAsync(IAuthParams authParams, string endPoint, HttpContent content);
        Task<HttpResponseMessage> PutAsync(IAuthParams authParams, string endPoint, HttpContent content, string callInitiator = null);
        void SetContentType(string contentType);
        void AddDefaultRequestHeader(string key, string value);
    }
}
