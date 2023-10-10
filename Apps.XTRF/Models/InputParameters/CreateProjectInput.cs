using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.InputParameters;

public class CreateProjectInput
{
    public string Name { get; set; }
    [Display("Client ID")] public string ClientId { get; set; }
    [Display("Service ID")] public string ServiceId { get; set; }
        
    [Display("External ID")]
    public string? ExternalId { get; set; }
}