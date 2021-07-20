using System.Threading.Tasks;

namespace Aether.Helpers.Interfaces
{
    public interface IAuditEventPublisher
    {
        Task Audit(string eventName, string targetId, string eventInitiator, string originalValue, string newValue);
    }
}