using System.Text.Json.Serialization;

namespace Aether.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnforcementType
    {
        None = 0,
        RightToKnow = 1,
        RightToAccess = 2,
        RightToDelete = 3
    }
}
