using Aether.Extensions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aether.Helpers
{
    public class MoriaPublisher : IMoriaPublisher
    {
        private readonly string _systemOfOrigin;
        private readonly IMoriaClient _client;

        public MoriaPublisher(string systemOfOrigin, IMoriaClient client)
        {
            _systemOfOrigin = Guard.Against.NullOrEmpty(systemOfOrigin, nameof(systemOfOrigin));
            _client         = Guard.Against.Null(client, nameof(client));
        }

        public async Task CaptureAuditEvent<T>(string eventName, string targetId, string eventInitiator, T originalValue, T newValue) =>
            await CaptureAuditEvent(eventName, targetId, eventInitiator, JsonConvert.SerializeObject(originalValue), JsonConvert.SerializeObject(newValue));

        public async Task CaptureAuditEvent(string eventName, string targetId, string eventInitiator, string originalValue, string newValue)
        {
            var evnt = new AuditEvent
            {
                SystemOfOrigin = _systemOfOrigin,
                EventName = eventName,
                EventCreateDate = DateTimeOffset.Now.ToUnixTimeSeconds(),
                TargetId = targetId,
                EventInitiator = eventInitiator,
                OriginalValue = originalValue,
                NewValue = newValue
            };

            await _client.CaptureAuditEvent(evnt);
        }

        public async Task CaptureAuditEvent<T>(string eventName, string targetId, T originalValue, T newValue, HttpRequest request) =>
            await CaptureAuditEvent(eventName, targetId, JsonConvert.SerializeObject(originalValue), JsonConvert.SerializeObject(newValue), request);

        public async Task CaptureAuditEvent(string eventName, string targetId, string originalValue, string newValue, HttpRequest request)
        {
            if (!request.IsTest())
            {
                request.Headers.TryGetValue(Constants.CALL_INITIATOR_HEADER_KEY, out var callInitiator);

                string callInitiatorString = callInitiator == StringValues.Empty ? "No Provided Initiator"
                                                                                 : callInitiator.ToString();
                var evnt = new AuditEvent
                {
                    SystemOfOrigin = _systemOfOrigin,
                    EventName = eventName,
                    EventCreateDate = DateTime.UtcNow.Ticks,
                    TargetId = targetId,
                    EventInitiator = callInitiatorString,
                    OriginalValue = originalValue,
                    NewValue = newValue
                };

                await _client.CaptureAuditEvent(evnt);
            }
        }

        public async Task CaptureDeleteAuditEvent(string eventName, string targetId, string eventInitiator) =>
            await CaptureAuditEvent(eventName, targetId, eventInitiator, null, "DELETED");

        public async Task CaptureDeleteAuditEvent(string eventName, string targetId, HttpRequest request) =>
            await CaptureAuditEvent(eventName, targetId, null, "DELETED", request);

        public async Task CaptureMetricEvent(HttpRequest request)
        {
            if (!request.IsTest())
            {
                request.Headers.TryGetValue(Constants.CALL_INITIATOR_HEADER_KEY, out var callInitiator);

                string callInitiatorString = callInitiator == StringValues.Empty ? "N/A"
                                                                                 : callInitiator.ToString();
                var evnt = new MetricEvent
                {
                    CalledBy = callInitiatorString,
                    CalledDate = DateTime.UtcNow,
                    CalledFrom = _systemOfOrigin,
                    Method = (HttpMethod) Enum.Parse(typeof(HttpMethod), request.Method, true),
                    Url = request.GetEncodedPathAndQuery()
                };

                await _client.CaptureMetricEvent(evnt);
            }
        }
    }
}
