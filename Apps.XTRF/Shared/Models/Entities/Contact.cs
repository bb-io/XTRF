using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Entities;

public class Contact
{
    public string Sms { get; set; }
    public string Fax { get; set; }
    public Emails Emails { get; set; }
    public IEnumerable<string> Phones { get; set; }
}

public class Emails
{
    public string Primary { get; set;  }
    
    [Display("CC")]
    public IEnumerable<string> Cc { get; set; }
    
    public IEnumerable<string> Additional { get; set; }
}