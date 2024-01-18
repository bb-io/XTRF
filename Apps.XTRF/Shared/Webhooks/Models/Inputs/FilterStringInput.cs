using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Webhooks.Models.Inputs;

public class FilterStringInput
{
    [Display("Filter string", Description = "String based on which results are filtered. For example, to filter " +
                                            "the results based on custom field with key 'field', you can specify " +
                                            "the following: 'customFields.field=ABC'")]
    public string? FilterString { get; set; }
}