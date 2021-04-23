using Aether.Models;

namespace Aether.Extensions
{
    public static class EnforcementRequestExtensions
    {
        public static object MakeObjectToLog(this EnforcementRequest enforcementRequest) =>
            enforcementRequest is null ? null
                                       : new
                                       {
                                           enforcementRequest.EnforcementRequestId,
                                           enforcementRequest.EnforcementType,
                                           enforcementRequest.HasSSN,
                                           enforcementRequest.IsTestMessage,
                                       };
    }
}
