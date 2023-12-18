using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.Project;

public class CreateProjectInput
{
    public string Name { get; set; }
    
    [Display("Service")]
    [DataSource(typeof(ServiceDataSourceHandler))]
    public string ServiceId { get; set; }
        
    [Display("Client ID")] 
    public string ClientId { get; set; }
    
    [Display("External ID")]
    public string? ExternalId { get; set; }
}