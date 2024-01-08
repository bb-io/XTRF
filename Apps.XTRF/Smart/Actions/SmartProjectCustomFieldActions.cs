using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Apps.XTRF.Smart.Actions.Base;
using Apps.XTRF.Smart.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Smart.Actions;

[ActionList]
public class SmartProjectCustomFieldActions : BaseSmartCustomFieldActions
{
    public SmartProjectCustomFieldActions(InvocationContext invocationContext) 
        : base(invocationContext, EntityType.Project)
    {
    }

    #region Get

    [Action("Smart: List custom fields for project", Description = "List the custom fields for a smart project, " +
                                                                   "returning fields that can be assigned to the " +
                                                                   "project without their values. To obtain the field " +
                                                                   "value, use the \"Get custom field for project\" " +
                                                                   "action corresponding to the field type")]
    public async Task<ListCustomFieldsResponse> ListCustomFieldsForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier)
        => await ListCustomFields(projectIdentifier.ProjectId);

    [Action("Smart: Get text or selection custom field for project",
        Description = "Retrieve a text or selection custom field for a smart project")]
    public async Task<CustomField<string>> GetTextCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier)
        => await GetTextCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Smart: Get number custom field for project", 
        Description = "Retrieve a number custom field for a smart project")]
    public async Task<CustomField<decimal?>> GetNumberCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier)
        => await GetNumberCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Smart: Get date custom field for project",
        Description = "Retrieve a date/date and time custom field for a smart project")]
    public async Task<CustomField<DateTime?>> GetDateCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier)
        => await GetDateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Smart: Get checkbox custom field for project",
        Description = "Retrieve a checkbox (boolean) custom field for a smart project")]
    public async Task<CustomField<bool?>> GetCheckboxCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier)
        => await GetCheckboxCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Smart: Get multiple selection custom field for project",
        Description = "Retrieve a multiple selection (list) custom field for a smart project")]
    public async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier)
        => await GetMultipleSelectionCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);
    
    #endregion

    #region Put

    [Action("Smart: Update text or selection custom field for project",
        Description = "Update a text or selection custom field for a smart project")]
    public async Task<SmartCustomFieldIdentifier> UpdateTextCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateTextCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update number custom field for project", 
        Description = "Update a number custom field for a smart project")]
    public async Task<SmartCustomFieldIdentifier> UpdateNumberCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] decimal value)
    {
        await UpdateNumberCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update date custom field for project",
        Description = "Update a date/date and time custom field for a smart project")]
    public async Task<SmartCustomFieldIdentifier> UpdateDateCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        await UpdateDateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update checkbox custom field for project",
        Description = "Update a checkbox (boolean) custom field for a smart project")]
    public async Task<SmartCustomFieldIdentifier> UpdateCheckboxCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] bool value)
    {
        await UpdateCheckboxCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Smart: Update multiple selection custom field for project",
        Description = "Update a multiple selection (list) custom field for a smart project")]
    public async Task<SmartCustomFieldIdentifier> UpdateMultipleSelectionCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] IEnumerable<string> value)
    {
        await UpdateMultipleSelectionCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    #endregion
}