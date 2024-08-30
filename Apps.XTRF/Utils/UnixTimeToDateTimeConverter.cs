using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Apps.XTRF.Utils;

public class UnixTimeToDateTimeConverter : JsonConverter<DateTime>
{
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        var unixTime = ((DateTimeOffset)value).ToUnixTimeMilliseconds();
        writer.WriteValue(unixTime);
    }

    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value != null && long.TryParse(reader.Value.ToString(), out long unixTime))
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTime).UtcDateTime;
        }
        return existingValue;
    }
}