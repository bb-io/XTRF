using Apps.XTRF.Classic.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Provider;

public class ProviderResponse
{
    [Display("Provider ID")]  
    public string Id { get; set; } = string.Empty;
    
    [Display("Provider ID number")]  
    public string IdNumber { get; set; } = string.Empty;
    
    [Display("Full name")]
    public string FullName { get; set; } = string.Empty;
    
    public string Notes { get; set; } = string.Empty;
    
    [Display("Billing address")]
    public AddressResponse BillingAddress { get; set; } = new();

    [Display("Correspondence address")]
    public AddressResponse CorrespondenceAddress { get; set; } = new();
    
    [Display("Contact information")]
    public ContactResponse Contact { get; set; } = new();
    
    [Display("Branch ID")]
    public string BranchId { get; set; } = string.Empty;
    
    [Display("Lead source ID")]
    public string LeadSourceId { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public CompetenciesResponse Competencies { get; set; } = new();

    [Display("Persons")]
    public List<Person> Persons { get; set; } = new();
}

public class AddressResponse
{
    [Display("Address line 1")]
    public string AddressLine1 { get; set; } = string.Empty;

    [Display("Address line 2")]
    public string AddressLine2 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    [Display("Postal code")]
    public string PostalCode { get; set; } = string.Empty;

    [Display("Province ID")]
    public string ProvinceId { get; set; } = string.Empty;

    [Display("country ID")]
    public string CountryId { get; set; } = string.Empty;
}

public class ContactResponse
{
    public List<string> Phones { get; set; } = new();

    public string Sms { get; set; } = string.Empty;

    public string Fax { get; set; } = string.Empty;

    public EmailsResponse Emails { get; set; } = new();

    public List<string> Websites { get; set; } = new();
}

public class EmailsResponse
{
    [Display("Primary email")]
    public string Primary { get; set; } = string.Empty;

    public List<string> Cc { get; set; } = new();

    public List<string> Additional { get; set; } = new();
}

public class CompetenciesResponse
{
    [Display("Language combinations")]
    public List<LanguageCombinationResponse> LanguageCombinations { get; set; } = new();
}

public class LanguageCombinationResponse
{
    [Display("Source language ID")]
    public string SourceLanguageId { get; set; } = string.Empty;

    [Display("Target language ID")]
    public string TargetLanguageId { get; set; } = string.Empty;
}