using System.Collections.Generic;
using Aether.Interfaces;
using Aether.Models.Kafka;

namespace Aether.Extensions.Models
{
    public static class IdentifierExtensions
    {
        public static IEnumerable<KafkaIdentifier> ToKafkaIdentifiers(this Dictionary<string, List<string>> identifiers)
        {
            foreach (var item in identifiers)
            {
                yield return new KafkaIdentifier
                {
                    Type = item.Key,
                    Value = item.Value
                };
            }
        }

        public static IEnumerable<KafkaIdentifier> ToKafkaIdentifiers(this IEnumerable<IIdentifier> identifiers)
        {
            foreach (var identifier in identifiers)
            {
                yield return identifier.ToKafkaIdentifier();
            }
        }

        public static KafkaIdentifier ToKafkaIdentifier(this IIdentifier identifier)
        {
            return new KafkaIdentifier
            {
                Type = identifier.IdentifierType.ToString(),
                Value = identifier.IdentifierValues
            };
        }
    }
}
