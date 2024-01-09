using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class CustomFieldFilterTypeDataHandler : XtrfInvocable, IDataSourceHandler
{
    private readonly CustomFieldFilterInput _filterInput;

    public CustomFieldFilterTypeDataHandler(InvocationContext invocationContext, 
        [WebhookParameter] CustomFieldFilterInput filterInput) : base(invocationContext)
    {
        _filterInput = filterInput;
    }

    public Dictionary<string, string> GetData(DataSourceContext context)
    {
        if (_filterInput.TextValue != null)
            return new()
            {
                { CustomFieldFilters.Equal, CustomFieldFilters.Equal },
                { CustomFieldFilters.Contains, CustomFieldFilters.Contains }
            };
        
        if (_filterInput.NumberValue != null)
            return new()
            {
                { CustomFieldFilters.Equal, CustomFieldFilters.Equal },
                { CustomFieldFilters.MoreThan, CustomFieldFilters.MoreThan },
                { CustomFieldFilters.LessThan, CustomFieldFilters.LessThan }
            };
        
        if (_filterInput.DateValue != null)
            return new()
            {
                { CustomFieldFilters.Equal, CustomFieldFilters.Equal },
                { CustomFieldFilters.Before, CustomFieldFilters.Before },
                { CustomFieldFilters.After, CustomFieldFilters.After }
            };
        
        if (_filterInput.CheckboxValue != null)
            return new()
            {
                { CustomFieldFilters.Equal, CustomFieldFilters.Equal }
            };

        throw new Exception("Please specify custom field value first.");
    }
}