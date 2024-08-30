using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Requests.Provider;

public class ProviderIdentifier
{
    [Display("Provider ID"), DataSource(typeof(ProviderDataSource))]
    public string ProviderId { get; set; } = string.Empty;
}