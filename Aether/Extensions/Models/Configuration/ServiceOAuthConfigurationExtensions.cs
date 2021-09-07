using Aether.Models.Configuration;
using RockLib.OAuth;

namespace Aether.Extensions.Models.Configuration
{
    public static class ServiceOAuthConfigurationExtensions
    {
        public static IAuthParams GetAuthParams(this ServiceOAuthConfiguration config) =>
            new Auth0AuthParams(config.ClientId, config.ClientSecret, config.Audience, config.AuthorizerUrl);
    }
}
