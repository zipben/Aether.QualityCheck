using System;
using Newtonsoft.Json;

namespace Aether.JsonConverters
{
    public class JsonInterfaceConverter<TConcrete, TInterface> : JsonConverter where TConcrete : TInterface
    {
        public override bool CanConvert(Type objectType) =>
            objectType is TInterface;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            serializer.Deserialize<TConcrete>(reader);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            serializer.Serialize(writer, value);
    }
}
