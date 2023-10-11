using Apps.XTRF.Utils.Converters;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Models.Responses.Entities;

public class SimpleCustomer
{
    [Display("ID")]
    [JsonConverter(typeof(IntToStringConverter))]
    public string Id { get; set; }
    public string Name { get; set; }
}