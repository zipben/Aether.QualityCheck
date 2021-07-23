using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Aether.Helpers.Interfaces
{
    public interface IAuditEventPublisher
    {
        Task CaptureAuditEvent(string eventName, string targetId, string eventInitiator, string originalValue, string newValue);
        Task CaptureAuditEvent(string eventName, string targetId, string originalValue, string newValue, HttpRequest request);
    }
}