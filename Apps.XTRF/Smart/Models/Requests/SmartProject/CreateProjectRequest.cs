using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Smart.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.SmartProject;

public class CreateProjectRequest
{
    public string Name { get; set; }
    
    [Display("Service")]
    [DataSource(typeof(SmartServiceDataSourceHandler))]
    public string ServiceId { get; set; }
        
    [Display("Client")] 
    [DataSource(typeof(CustomerDataHandler))]
    public string ClientId { get; set; }
}