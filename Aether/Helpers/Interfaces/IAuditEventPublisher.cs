using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Aether.Helpers.Interfaces
{
    public interface IAuditEventPublisher
    {
        Task CaptureAuditEvent(string eventName, string targetId, string eventInitiator, string originalValue, string newValue);
        Task CaptureAuditEvent<T>(string eventName, string targetId, string eventInitiator, T originalValue, T newValue);
        Task CaptureAuditEvent(string eventName, string targetId, string originalValue, string newValue, HttpRequest request);
        Task CaptureAuditEvent<T>(string eventName, string targetId, T originalValue, T newValue, HttpRequest request);

        Task CaptureDeleteAuditEvent(string eventName, string targetId, string eventInitiator);
        Task CaptureDeleteAuditEvent(string eventName, string targetId, HttpRequest request);
    }
}