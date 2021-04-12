using Aether.Models;

namespace Aether.Extensions
{
    public static class EntityEnforcementRequestExtensions
    {
        public static object MakeObjectToLog(this EntityEnforcementRequest entityEnforcementRequest) =>
            new
            {
                entityEnforcementRequest.EnforcementRequestId,
                entityEnforcementRequest.EnforcementType,
                entityEnforcementRequest.SystemName,
                entityEnforcementRequest.ResponseEndpoint
            };
    }
}
