using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Requests.Customer;

public class CreateContactRequest
{
    public string Name { get; set; }
        
    [Display("Last name")]
    public string LastName { get; set; }
    
    [Display("Primary email")]
    public string Email { get; set; }
    
    public IEnumerable<string>? Phones { get; set; }
    
    [Display("Additional emails")]
    public IEnumerable<string>? AdditionalEmails { get; set; }
    
    [Display("Mother tongues")]
    [DataSource(typeof(LanguageDataHandler))]
    public IEnumerable<string>? MotherTonguesIds { get; set; }
}