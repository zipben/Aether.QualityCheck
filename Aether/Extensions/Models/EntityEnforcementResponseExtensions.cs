using Aether.Models;

namespace Aether.Extensions
{
    public static class EntityEnforcementResponseExtensions
    {
        public static object MakeObjectToLog(this EntityEnforcementResponse entityEnforcementResponse) =>
            entityEnforcementResponse is null ? null
                                              : new
                                                {
                                                    entityEnforcementResponse.EnforcementRequestId, 
                                                    entityEnforcementResponse.SendingSystemName 
                                                };
    }
}
