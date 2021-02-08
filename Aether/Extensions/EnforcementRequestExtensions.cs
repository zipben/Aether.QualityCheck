using Aether.Models;

namespace Aether.Extensions
{
    public static class EnforcementRequestExtensions
    {
        public static object MakeObjectToLog(this EnforcementRequest enforcementRequest) =>
            new
            {
                enforcementRequest.EnforcementRequestId,
                enforcementRequest.EnforcementType,
                enforcementRequest.HasSSN,
                enforcementRequest.IsTestMessage,
            };
    }
}
