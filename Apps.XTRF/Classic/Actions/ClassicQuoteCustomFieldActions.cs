using Apps.XTRF.Classic.Actions.Base;
using Apps.XTRF.Classic.Models.Identifiers;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Classic.Actions;

[ActionList]
public class ClassicQuoteCustomFieldActions : BaseClassicCustomFieldActions
{
    public ClassicQuoteCustomFieldActions(InvocationContext invocationContext) 
        : base(invocationContext, EntityType.Quote)
    {
    }
    
    #region Get

    [Action("Classic: List custom fields for quote", Description = "List the custom fields for a classic quote, " +
                                                                   "returning fields that can be assigned to the quote " +
                                                                   "without their values. To obtain the field value, " +
                                                                   "use the \"Get custom field for quote\" action " +
                                                                   "corresponding to the field type")]
    public async Task<ListCustomFieldsResponse> ListCustomFieldsForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier)
        => await ListCustomFields(quoteIdentifier.QuoteId);

    [Action("Classic: Get text or selection custom field for quote",
        Description = "Retrieve a text or selection custom field for a classic quote")]
    public async Task<CustomField<string>> GetTextCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetTextCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get number custom field for quote", 
        Description = "Retrieve a number custom field for a classic quote")]
    public async Task<CustomField<decimal?>> GetNumberCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetNumberCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get date custom field for quote",
        Description = "Retrieve a date/date and time custom field for a classic quote")]
    public async Task<CustomField<DateTime?>> GetDateCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetDateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get checkbox custom field for quote",
        Description = "Retrieve a checkbox (boolean) custom field for a classic quote")]
    public async Task<CustomField<bool?>> GetCheckboxCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetCheckboxCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get multiple selection custom field for quote",
        Description = "Retrieve a multiple selection (list) custom field for a classic quote")]
    public async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetMultipleSelectionCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);
    
    #endregion
    
    #region Put

    [Action("Classic: Update text custom field for quote",
        Description = "Update a text custom field for a classic quote")]
    public async Task<ClassicCustomFieldIdentifier> UpdateTextCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateTextCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, customFieldIdentifier.Name,
            value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update number custom field for quote", 
        Description = "Update a number custom field for a classic quote")]
    public async Task<ClassicCustomFieldIdentifier> UpdateNumberCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] decimal value)
    {
        await UpdateNumberCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, customFieldIdentifier.Name,
            value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update date custom field for quote",
        Description = "Update a date custom field for a classic quote")]
    public async Task<ClassicCustomFieldIdentifier> UpdateDateCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        await UpdateDateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, customFieldIdentifier.Name,
            value);
        return customFieldIdentifier;
    }
    
    [Action("Classic: Update date and time custom field for quote",
        Description = "Update a date and time custom field for a classic quote")]
    public async Task<ClassicCustomFieldIdentifier> UpdateDateTimeCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        await UpdateDateTimeCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key,
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update checkbox custom field for quote",
        Description = "Update a checkbox (boolean) custom field for a classic quote")]
    public async Task<ClassicCustomFieldIdentifier> UpdateCheckboxCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] bool value)
    {
        await UpdateCheckboxCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key,
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }
    
    [Action("Classic: Update selection custom field for quote",
        Description = "Update a selection custom field for a classic quote")]
    public async Task<ClassicCustomFieldIdentifier> UpdateSelectionCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateSelectionCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, 
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }
    
    [Action("Classic: Update multiple selection custom field for quote",
        Description = "Update a multiple selection (list) custom field for a classic quote")]
    public async Task<ClassicCustomFieldIdentifier> UpdateMultipleSelectionCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] IEnumerable<string> value)
    {
        await UpdateMultipleSelectionCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, 
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }
    
    #endregion
}