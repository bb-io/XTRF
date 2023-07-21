using Newtonsoft.Json;

namespace Apps.XTRF.Utils.Converters;

public class IntToStringConverter : JsonConverter<string>
{
    public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue,
        JsonSerializer serializer)
        => reader.Value.ToString();

    public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }
}