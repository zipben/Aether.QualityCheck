using Aether.Interfaces.Themis;

namespace Aether.Extensions.Models
{
    public static class LitigationExtensions
    {
        public static bool HasInputIdentifiers(this ILitigation litigation) =>
            litigation.InputIdentifiers != null && litigation.InputIdentifiers.Count != 0;
    }
}
