using Aether.Enums;

namespace Aether.Models
{
    public class ServiceSettings
    {
        public string AuthorizerUrl { get; set; }
        public string BaseUrl { get; set; }
        public GrantType GrantType { get; set; }
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UseBasicAuthentication { get; set; }
        public bool SendParametersInQueryString { get; set; }
    }
}
