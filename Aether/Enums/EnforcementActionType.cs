using System.Text.Json.Serialization;

namespace Aether.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnforcementActionType
    {
        ActionTaken = 1,
        ActionNotTaken = 2,
        NotFound = 3,
        ActionPartiallyTaken = 4
    }
}
