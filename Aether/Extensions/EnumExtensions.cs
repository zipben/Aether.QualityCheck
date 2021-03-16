using Aether.Enums;
using Aether.Helpers;

namespace Aether.Extensions
{
    public static class EnumExtensions
    {
        public static string GetFriendlyDescription(this EnforcementActionType action, EnforcementType type) =>
            type switch
            {
                EnforcementType.RightToDelete =>
                    action switch
                    {
                        EnforcementActionType.ActionTaken => "Identifiers Deleted",
                        EnforcementActionType.ActionNotTaken => "Identifiers Not Deleted",
                        EnforcementActionType.NotFound => "Identifiers Not Found",
                        EnforcementActionType.ActionPartiallyTaken => "Some Identifiers Deleted",
                        _ => string.Empty,
                    },
                _ => "N/A"
            };

        public static string GetFriendlyDescription(this EnforcementType type) => EnumHelpers.GetFriendlyName(type);
    }
}
