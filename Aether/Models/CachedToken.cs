using System;

namespace Aether.Models
{
    public class CachedToken
    {
        public string Token { get; set; }
        public string AuthorizationUrl { get; set; }
        public string User { get; set; }
        public string Aud { get; set; }
        public string Scope { get; set; }
        public DateTime DateCached { get; set; }
    }
}
