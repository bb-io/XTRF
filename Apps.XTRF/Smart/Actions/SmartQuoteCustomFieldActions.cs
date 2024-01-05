﻿using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Apps.XTRF.Smart.Actions.Base;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Smart.Actions;

[ActionList]
public class SmartQuoteCustomFieldActions : BaseSmartCustomFieldActions
{
    public SmartQuoteCustomFieldActions(InvocationContext invocationContext) : base(invocationContext, EntityType.Quote)
    {
    }

    #region Get

    [Action("Smart: List custom fields for quote", Description = "List the custom fields for a smart quote, returning " +
                                                                 "fields that can be assigned to the quote without " +
                                                                 "their values. To obtain the field value, use the " +
                                                                 "\"Get custom field for quote\" action corresponding " +
                                                                 "to the field type")]
    public async Task<ListCustomFieldsResponse> ListCustomFieldsForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier)
        => await ListCustomFields(quoteIdentifier.QuoteId);

    [Action("Smart: Get text or selection custom field for quote",
        Description = "Retrieve a text or selection custom field for a smart quote")]
    public async Task<CustomField<string>> GetTextCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetTextCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Smart: Get number custom field for quote", 
        Description = "Retrieve a number custom field for a smart quote")]
    public async Task<CustomField<decimal?>> GetNumberCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetNumberCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Smart: Get date custom field for quote",
        Description = "Retrieve a date/date and time custom field for a smart quote")]
    public async Task<CustomField<DateTime?>> GetDateCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetDateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Smart: Get checkbox custom field for quote",
        Description = "Retrieve a checkbox (boolean) custom field for a smart quote")]
    public async Task<CustomField<bool?>> GetCheckboxCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetCheckboxCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    [Action("Smart: Get multiple selection custom field for quote",
        Description = "Retrieve a multiple selection (list) custom field for a smart quote")]
    public async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetMultipleSelectionCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key);

    #endregion
    
    #region Put

    [Action("Smart: Update text or selection custom field for quote",
        Description = "Update a text or selection custom field for a smart quote")]
    public async Task<CustomFieldIdentifier> UpdateTextCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateTextCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update number custom field for quote", 
        Description = "Update a number custom field for a smart quote")]
    public async Task<CustomFieldIdentifier> UpdateNumberCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] decimal value)
    {
        await UpdateNumberCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update date custom field for quote",
        Description = "Update a date/date and time custom field for a smart quote")]
    public async Task<CustomFieldIdentifier> UpdateDateCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        await UpdateDateCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update checkbox custom field for quote",
        Description = "Update a checkbox (boolean) custom field for a smart quote")]
    public async Task<CustomFieldIdentifier> UpdateCheckboxCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] bool value)
    {
        await UpdateCheckboxCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update multiple selection custom field for quote",
        Description = "Update a multiple selection (list) custom field for a smart quote")]
    public async Task<CustomFieldIdentifier> UpdateMultipleSelectionCustomFieldForQuote(
        [ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] IEnumerable<string> value)
    {
        await UpdateMultipleSelectionCustomField(quoteIdentifier.QuoteId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    #endregion
}