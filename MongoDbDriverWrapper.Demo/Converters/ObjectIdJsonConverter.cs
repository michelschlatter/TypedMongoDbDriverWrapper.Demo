using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace MongoDbDriverWrapper.Demo.Api.Converters
{
    public class ObjectIdJsonConverter : JsonConverter<ObjectId>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(ObjectId);
        }

        public override ObjectId Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                ObjectId.Parse(reader.GetString() ?? ObjectId.Empty.ToString());

        public override void Write(
            Utf8JsonWriter writer,
            ObjectId objectId,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(objectId.ToString());
    }
}
