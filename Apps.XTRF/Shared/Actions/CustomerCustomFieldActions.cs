﻿using Apps.XTRF.Classic.Actions.Base;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Shared.Actions;

[ActionList]
public class CustomerCustomFieldActions : BaseClassicCustomFieldActions
{
    public CustomerCustomFieldActions(InvocationContext invocationContext) 
        : base(invocationContext, EntityType.Customer)
    {
    }
    
    #region Get

    [Action("List custom fields for customer", Description = "List the custom fields for a customer, returning fields " +
                                                             "that can be assigned to the customer without their " +
                                                             "values. To obtain the field value, use the \"Get custom " +
                                                             "field for customer\" action corresponding to the field type")]
    public async Task<ListCustomFieldsResponse> ListCustomFieldsForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier)
        => new(await ListCustomFields(customerIdentifier.CustomerId));

    [Action("Get text or selection custom field for customer", Description = "Retrieve a text or selection custom " +
                                                                             "field for a customer")]
    public async Task<CustomField<string>> GetTextCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetTextCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key);

    [Action("Get number custom field for customer", Description = "Retrieve a number custom field for a customer")]
    public async Task<CustomField<decimal?>> GetNumberCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetNumberCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key);

    [Action("Get date custom field for customer", Description = "Retrieve a date/date and time custom field for " +
                                                                "a customer")]
    public async Task<CustomField<DateTime?>> GetDateCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetDateCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key);

    [Action("Get checkbox custom field for customer", Description = "Retrieve a checkbox (boolean) custom field for " +
                                                                    "a customer")]
    public async Task<CustomField<bool?>> GetCheckboxCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetCheckboxCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key);

    [Action("Get multiple selection custom field for customer", Description = "Retrieve a multiple selection (list) " +
                                                                              "custom field for a customer")]
    public async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetMultipleSelectionCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key);
    
    #endregion
    
    #region Put

    [Action("Update text or selection custom field for customer", 
        Description = "Update a text or selection custom field for a customer")]
    public async Task<CustomFieldIdentifier> UpdateTextCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Update number custom field for customer", Description = "Update a number custom field for a customer")]
    public async Task<CustomFieldIdentifier> UpdateNumberCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] decimal value)
    {
        await UpdateCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Update date custom field for customer", Description = "Update a date/date and time custom field for " +
                                                                   "a customer")]
    public async Task<CustomFieldIdentifier> UpdateDateCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        await UpdateCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key, 
            new LongDateTimeRepresentation(value.ConvertToUnixTime()));
        return customFieldIdentifier;
    }

    [Action("Update checkbox custom field for customer", Description = "Update a checkbox (boolean) custom field for " +
                                                                       "a customer")]
    public async Task<CustomFieldIdentifier> UpdateCheckboxCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] bool value)
    {
        await UpdateCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Update multiple selection custom field for customer", 
        Description = "Update a multiple selection (list) custom field for a customer")]
    public async Task<CustomFieldIdentifier> UpdateMultipleSelectionCustomFieldForCustomer(
        [ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] IEnumerable<string> value)
    {
        await UpdateCustomField(customerIdentifier.CustomerId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }
    
    #endregion
}