using Aether.Models;
using Aether.Models.RightRequestWorkflow;

namespace Aether.Extensions
{
    public static class EntityEnforcementRequestExtensions
    {
        public static object MakeObjectToLog(this EntityEnforcementRequest entityEnforcementRequest) =>
            entityEnforcementRequest is null ? null
                                             : new
                                               {
                                                   entityEnforcementRequest.EnforcementRequestId,
                                                   entityEnforcementRequest.EnforcementType,
                                                   entityEnforcementRequest.SystemName,
                                                   entityEnforcementRequest.ResponseEndpoint
                                               };
    }
}
