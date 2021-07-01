using System.Collections.Generic;
using Aether.Interfaces;
using Aether.Models.Kafka;

namespace Aether.Extensions.Models
{
    public static class IdentifierExtensions
    {
        public static IEnumerable<KafkaIdentifier> MakeKafkaIdentifiers(this IEnumerable<IIdentifier> identifiers)
        {
            foreach (var identifier in identifiers)
            {
                yield return identifier.MakeKafkaIdentifier();
            }
        }

        public static KafkaIdentifier MakeKafkaIdentifier(this IIdentifier identifier)
        {
            return new KafkaIdentifier
            {
                Type = identifier.IdentifierType.ToString(),
                Value = identifier.IdentifierValues
            };
        }
    }
}
