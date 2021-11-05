using System.Threading.Tasks;
using Aether.Interfaces.Moria;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface IMoriaClient
    {
        Task CaptureAuditEvent(IAuditEvent auditEvent);
        Task CaptureMetricEvent(IMetricEvent metricEvent);
    }
}
