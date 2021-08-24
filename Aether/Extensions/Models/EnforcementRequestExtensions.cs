using Aether.Models.RightRequestWorkflow;
using Aether.ServiceSpecificStaticValues;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<string> GetDiagnosticSystemNames(this EnforcementRequest enforcementRequest) =>
            enforcementRequest.DiagnosticFlags
                .Where(d => d.StartsWith(OyaStaticValues.DiagnosticFlags.SYSTEM_NAME_PREFIX))
                .Select(d => d.Split(":")[1]);

        public static void AddDiagnosticSystemNames(this EnforcementRequest enforcementRequest, IEnumerable<string> systemNames) =>
            enforcementRequest.DiagnosticFlags.AddRange(
                systemNames.Select(name => OyaStaticValues.DiagnosticFlags.SYSTEM_NAME_PREFIX + name));
    }
}
