using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Requests.Customer;

public class UpdateCustomerRequest
{
    public string? Name { get; set; }

    [Display("Full name")]
    public string? FullName { get; set; }

    public string? Notes { get; set; }

    [StaticDataSource(typeof(ClientStatusDataSource))]
    public string? Status { get; set; }

    [Display("Primary email")]
    public string? Email { get; set; }

    [Display("Phone numbers")]
    public IEnumerable<string>? Phones { get; set; }

    [Display("Additional emails")]
    public IEnumerable<string>? AdditionalEmails { get; set; }

    [Display("Address line 1")]
    public string? AddressLine1 { get; set; }

    [Display("Address line 2")]
    public string? AddressLine2 { get; set; }

    [Display("Country ID")]
    //[DataSource(typeof(CountryDataHandler))]
    public string? CountryId { get; set; }

    [Display("Region/Province ID")]
    public string? ProvinceId { get; set; }

    [Display("City")]
    public string? City { get; set; }

    [Display("Postal code")]
    public string? PostalCode { get; set; }

    [Display("Time zone")]
    public string? TimeZone { get; set; }
    [Display("Use billing address for correspondence")]
    public bool? UseBillingAddress { get; set; }
    [Display("Tax rate")]
    public string? TaxRate { get; set; }

    [Display("Preferred email language ID")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? PreferredEmailLanguageId { get; set; }

    [Display("Salesperson responsible ID")]
    public string? SalesPersonId { get; set; }
}