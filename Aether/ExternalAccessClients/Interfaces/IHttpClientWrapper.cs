using System.Net.Http;
using System.Threading.Tasks;
using RockLib.OAuth;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface IHttpClientWrapper
    {
        void SetBaseURI(string endpoint);
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> GetAsync(Auth0AuthParams auth0Auth, string requestUri);
        Task<HttpResponseMessage> PostAsync(IAuthParams authParams, string endPoint, HttpContent content);
        Task<HttpResponseMessage> PutAsync(IAuthParams authParams, string endPoint, HttpContent content);
        Task<HttpResponseMessage> PatchAsync(IAuthParams authParams, string endPoint, HttpContent content);
        void SetContentType(string contentType);
    }
}
