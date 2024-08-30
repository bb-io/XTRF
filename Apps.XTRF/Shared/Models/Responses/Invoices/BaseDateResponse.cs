using Apps.XTRF.Utils;
using Newtonsoft.Json;

namespace Apps.XTRF.Shared.Models.Responses.Invoices;

public class BaseDateResponse
{
    [JsonProperty("time"), JsonConverter(typeof(UnixTimeToDateTimeConverter))]
    public virtual DateTime Date { get; set; }
}