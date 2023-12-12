using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Identifiers;

public class VendorIdentifier
{
    [Display("Vendor")]
    [DataSource(typeof(VendorDataHandler))]
    public string VendorId { get; set; }
}