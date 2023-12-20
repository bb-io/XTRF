using Apps.XTRF.Shared.Utils.Converters;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Shared.Models.Entities;

public class SimpleCustomer
{
    [Display("Customer")]
    [JsonConverter(typeof(IntToStringConverter))]
    public string Id { get; set; }
    
    public string Name { get; set; }
}