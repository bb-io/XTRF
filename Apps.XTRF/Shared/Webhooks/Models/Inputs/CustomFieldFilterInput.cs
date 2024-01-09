using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Webhooks.Models.Inputs;

public class CustomFieldFilterInput
{
    [Display("Custom field key", Description = "If specified, custom field value should also be provided; only a single " +
                                               "custom field value must be specified (e.g. if 'Custom field text value' " +
                                               "is specified, no other custom field value parameter should be specified)")]
    public string? Key { get; set; }
    
    [Display("Custom field filter type", Description = "Type of filter to apply to the actual custom field value; " +
                                                       "defaults to 'Equal,' except when 'Custom field key' belongs " +
                                                       "to multiple selection and 'Custom field text value' is " +
                                                       "specified; in this case, the filter type is always 'Contains'")]
    [DataSource(typeof(CustomFieldFilterTypeDataHandler))]
    public string? FilterType { get; set; }
    
    [Display("Custom field text value", Description = "Value of text, selection or one of the multiple selection items " +
                                                      "custom field")]
    public string? TextValue { get; set; }
    
    [Display("Custom field number value", Description = "Value of number custom field")]
    public decimal? NumberValue { get; set; }
    
    [Display("Custom field date value", Description = "Value of date/date and time custom field")]
    public DateTime? DateValue { get; set; }
    
    [Display("Custom field checkbox value", Description = "Value of checkbox (boolean) custom field")]
    public bool? CheckboxValue { get; set; }
}