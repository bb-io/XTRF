using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Provider;

public class ProviderSearchRequest
{
    [Display("Provider ID number")]
    public string? IdNumber { get; set; }
}