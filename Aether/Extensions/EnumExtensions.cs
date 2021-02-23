using Aether.Enums;

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
                        _ => string.Empty,
                    },
                _ => "N/A"
            };

        public static string GetFriendlyDescription(this EnforcementType type) =>
            type switch
            {
                EnforcementType.RightToAccess => "Right To Access",
                EnforcementType.RightToDelete => "Right To Delete",
                EnforcementType.RightToKnow => "Right To Know",
                _ => string.Empty
            };
    }
}
