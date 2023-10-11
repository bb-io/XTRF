using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Requests.CustomField;

public class UpdateCustomFieldInput
{
    [Display("Custom field key")] public string Key { get; set; }

    [Display("Custom field new value")] public string Value { get; set; }
}