using Aether.Models;

namespace Aether.Extensions
{
    public static class EnforcementResponseExtensions
    {
        public static object MakeObjectToLog(this EnforcementResponse enforcementResponse) =>
            enforcementResponse is null ? null
                                        : new
                                          {
                                              enforcementResponse.EnforcementRequestId,
                                              enforcementResponse.IsTestMessage
                                          };
    }
}
