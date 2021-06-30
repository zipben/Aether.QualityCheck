using System.Threading.Tasks;
using Aether.Models;
using Aether.Models.Kafka;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface IKafkaNotifier
    {
        public Task<NotificationResult> SendAsync(BaseKafkaMessage messageContent, string schemaPath, string topic);
    }
}
