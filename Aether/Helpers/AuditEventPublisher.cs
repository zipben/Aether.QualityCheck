using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models;
using Ardalis.GuardClauses;
using System;
using System.Threading.Tasks;

namespace Aether.Helpers
{
    public class AuditEventPublisher : IAuditEventPublisher
    {
        private readonly string _systemOfOrigin;
        private readonly IAuditClient _client;

        public AuditEventPublisher(string systemOfOrigin, IAuditClient client)
        {
            _systemOfOrigin = Guard.Against.NullOrEmpty(systemOfOrigin, nameof(systemOfOrigin));
            _client         = Guard.Against.Null(client, nameof(client));
        }

        public async Task CaptureAuditEvent(string eventName, string targetId, string eventInitiator, string originalValue, string newValue)
        {
            var evnt = new AuditEvent()
            {
                SystemOfOrigin = _systemOfOrigin,
                EventName = eventName,
                EventCreateDate = DateTime.UtcNow.Ticks,
                TargetId = targetId,
                EventInitiator = eventInitiator,
                OriginalValue = originalValue,
                NewValue = newValue
            };

            await _client.CaptureAuditEvent(evnt);
        }
    }
}
