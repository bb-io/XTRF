using Apps.XTRF.Classic.Actions.Base;
using Apps.XTRF.Classic.Models.Identifiers;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Classic.Actions;

[ActionList("Classic: quote custom fields")]
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
        => new(await ListCustomFields(quoteIdentifier.QuoteId));

    [Action("Classic: Get text or selection custom field for quote",
        Description = "Retrieve a text or selection custom field for a classic quote")]
    public async Task<CustomField<string>> GetTextCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier)
        => await GetTextCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get number custom field for quote", 
        Description = "Retrieve a number custom field for a classic quote")]
    public async Task<CustomDecimalField> GetNumberCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier)
        => await GetNumberCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get date custom field for quote",
        Description = "Retrieve a date/date and time custom field for a classic quote")]
    public async Task<CustomDateTimeField> GetDateCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier)
        => await GetDateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get checkbox custom field for quote",
        Description = "Retrieve a checkbox (boolean) custom field for a classic quote")]
    public async Task<CustomBooleanField> GetCheckboxCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier)
        => await GetCheckboxCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Classic: Get multiple selection custom field for quote",
        Description = "Retrieve a multiple selection (list) custom field for a classic quote")]
    public async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier)
        => await GetMultipleSelectionCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);
    
    #endregion
    
    #region Put

    [Action("Classic: Update text or selection custom field for quote",
        Description = "Update a text or selection custom field for a classic quote")]
    public async Task<ClassicQuoteCustomFieldIdentifier> UpdateTextCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update number custom field for quote", 
        Description = "Update a number custom field for a classic quote")]
    public async Task<ClassicQuoteCustomFieldIdentifier> UpdateNumberCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] decimal value)
    {
        await UpdateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update date custom field for quote",
        Description = "Update a date/date and time custom field for a classic quote")]
    public async Task<ClassicQuoteCustomFieldIdentifier> UpdateDateCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        var timeZoneInfo = await GetTimeZoneInfo();
        await UpdateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, 
            new LongDateTimeRepresentation(value.ConvertToUnixTime(timeZoneInfo)));
        return customFieldIdentifier;
    }

    [Action("Classic: Update checkbox custom field for quote",
        Description = "Update a checkbox (boolean) custom field for a classic quote")]
    public async Task<ClassicQuoteCustomFieldIdentifier> UpdateCheckboxCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] bool value)
    {
        await UpdateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update multiple selection custom field for quote",
        Description = "Update a multiple selection (list) custom field for a classic quote")]
    public async Task<ClassicQuoteCustomFieldIdentifier> UpdateMultipleSelectionCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicQuoteCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] IEnumerable<string> value)
    {
        await UpdateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }
    
    #endregion
}