using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Aether.Extensions;
using Aether.Models;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Aether.Models.Configuration;
using Aether.Models.Kafka;
using Aether.ExternalAccessClients.Interfaces;

namespace Aether.ExternalAccessClients
{
    public class KafkaNotifier : IKafkaNotifier
    {
        private readonly IApiLogger _apiLogger;
        private readonly KafkaSettings _config;
        private readonly IProducer<string, string> _kafkaProducer;

        public KafkaNotifier(IApiLogger apiLogger,
                             IOptions<KafkaSettings> config,
                             IProducer<string, string> kafkaProducer)
        {
            _apiLogger =        Guard.Against.Null(apiLogger, nameof(apiLogger));
            _config =           Guard.Against.Null(config?.Value, nameof(config.Value));
            _kafkaProducer =    Guard.Against.Null(kafkaProducer, nameof(kafkaProducer));

            _apiLogger.Method.CallingClassName = nameof(KafkaNotifier);
        }

        public async Task<NotificationResult> SendAsync(BaseKafkaMessage messageContent, string schemaPath)
        {
            _apiLogger.Method.Begin(out var methodName, 
                                    out var objectToLog, 
                                    new { HostName = _config?.HostName, Topic = _config?.Topic, UserName = _config?.UserName, Id = messageContent?.Id, Subject = messageContent?.Subject }, 
                                    nameof(SendAsync));

            Guard.Against.Null(messageContent, nameof(messageContent));
            Guard.Against.NullOrWhiteSpace(messageContent.Id, nameof(messageContent.Id));
            Guard.Against.NullOrWhiteSpace(messageContent.DataContentType, nameof(messageContent.DataContentType));
            Guard.Against.NullOrWhiteSpace(messageContent.DataSchema, nameof(messageContent.DataSchema));
            Guard.Against.NullOrWhiteSpace(messageContent.Source, nameof(messageContent.Source));
            Guard.Against.NullOrWhiteSpace(messageContent.SpecVersion, nameof(messageContent.SpecVersion));
            Guard.Against.NullOrWhiteSpace(messageContent.Subject, nameof(messageContent.Subject));
            Guard.Against.NullOrWhiteSpace(schemaPath, nameof(schemaPath));

            NotificationResult notificationResult = null;

            var serializedKafkaMessage = JsonConvert.SerializeObject(messageContent);

            if (ValidateMessagePerSchema(serializedKafkaMessage, schemaPath))
            {
                var message = new Message<string, string>
                {
                    Key = messageContent.Id,
                    Value = serializedKafkaMessage
                };

                notificationResult = await TrySendAsync(message);
            }
            else
            {
                notificationResult = NotificationResult.Failure(messageContent.Id, $"Message is not valid per schema: {schemaPath}");
            }

            _apiLogger.Method.End(new { Key = messageContent.Id, notificationResult }, methodName);
            return notificationResult;
        }

        private bool ValidateMessagePerSchema(string serializedMessage, string schemaPath)
        {
            string jsonSchema = File.ReadAllText(schemaPath);

            JSchema schema = JSchema.Parse(jsonSchema);
            JObject jObject = JObject.Parse(serializedMessage);

            var messageIsValid = jObject.IsValid(schema, out IList<string> errors);
            if (!messageIsValid)
            {
                _apiLogger.LogError(nameof(ValidateMessagePerSchema), new { Errors = errors });
            }
            return messageIsValid;
        }

        private async Task<NotificationResult> TrySendAsync(Message<string, string> message)
        {
            _apiLogger.Method.Begin(out var methodName, out var objectToLog, new { Id = message.Key }, nameof(TrySendAsync));

            DeliveryResult<string, string> result = null;
            NotificationResult notificationResult = null;

            try
            {
                if (_config.Topic.Exists())
                {
                    result = await _kafkaProducer.ProduceAsync(_config.Topic, message);

                    if (result is null)
                    {
                        notificationResult = NotificationResult.Failure(message.Key, $"Kafka did not respond for {message.Key}");
                        _apiLogger.LogError(notificationResult.ResponseDetails, message);
                    }
                    else if (result.Status == PersistenceStatus.NotPersisted)
                    {
                        notificationResult = NotificationResult.Failure(message.Key, $"The message was not persisted for {message.Key}");
                        _apiLogger.LogError(notificationResult.ResponseDetails, message);
                    }
                    else if (result.Status == PersistenceStatus.PossiblyPersisted)
                    {
                        notificationResult = NotificationResult.Failure(message.Key, $"The message may or may not not have been persisted for {message.Key}");
                        _apiLogger.LogWarning(notificationResult.ResponseDetails, result);
                    }
                    else
                    {
                        notificationResult = NotificationResult.Success($"Kafka received the message successfully for {message.Key}");
                    }
                }
                else
                {
                    notificationResult = NotificationResult.Failure(message.Key, $"Kafka Topic is empty for {message.Key}");
                    _apiLogger.LogWarning(notificationResult.ResponseDetails, result);
                }
            }
            catch (Exception ex)
            {
                notificationResult = NotificationResult.Failure(message.Key, $"Kafka threw an exception for {message.Key}: {ex.Message}");
                _apiLogger.LogError(notificationResult.ResponseDetails, objectToLog, ex);
            }

            _apiLogger.Method.End(new { Status = result?.Status, notificationResult }, methodName);
            return notificationResult;
        }
    }
}
