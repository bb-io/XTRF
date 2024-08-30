using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Requests.Provider;

public class ProviderPersonIdentifier
{
    [Display("Provider person ID"), DataSource(typeof(ProviderPersonDataSource))]
    public string ProviderPersonId { get; set; } = string.Empty;
}