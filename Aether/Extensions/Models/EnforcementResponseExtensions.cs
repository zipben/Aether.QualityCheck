using Aether.Models;

namespace Aether.Extensions
{
    public static class EnforcementResponseExtensions
    {
        public static object MakeObjectToLog(this EnforcementResponse enforcementResponse) =>
            new
            {
                enforcementResponse.EnforcementRequestId,
                enforcementResponse.IsTestMessage
            };
    }
}
