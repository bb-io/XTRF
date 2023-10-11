using Apps.XTRF.Utils.Converters;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Models.Responses.Project;

public class GetClientContactsByProjectResponse
{
    [Display("Primary ID")]  
    [JsonConverter(typeof(IntToStringConverter))]
    public string PrimaryId { get; set; }
        
    [Display("Additional IDs")]  public IEnumerable<int> AdditionalIds { get; set; }
}