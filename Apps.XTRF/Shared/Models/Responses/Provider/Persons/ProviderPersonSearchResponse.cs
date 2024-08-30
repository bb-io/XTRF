using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Provider.Persons;

public class ProviderPersonSearchResponse
{
    [Display("Provider persons")]
    public List<ProviderPersonResponse> ProviderPersons { get; set; } = new();
}