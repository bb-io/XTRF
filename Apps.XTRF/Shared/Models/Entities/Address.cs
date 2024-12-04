using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Entities;

public class Address
{
    [Display("Address line 1")]
    public string AddressLine1 { get; set; }
    
    [Display("Address line 2")]
    public string AddressLine2 { get; set; }
    
    public string City { get; set; }
    
    [Display("Postal code")]
    public string PostalCode { get; set; }

    [Display("Province ID")]
    public string ProvinceId { get; set; }

    [Display("Country ID")]
    public string CountryId { get; set; }
}