using Apps.XTRF.Utils.Converters;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Models.Responses.Entities;

public class Address
{
    [Display("Address line 1")]
    public string AddressLine1 { get; set; }
    
    [Display("Address line 2")]
    public string AddressLine2 { get; set; }
    
    public string City { get; set; }
    
    [Display("Postal code")]
    public string PostalCode { get; set; }
    
    [Display("Country ID")]
    [JsonConverter(typeof(IntToStringConverter))]
    public string CountryId { get; set; }
}