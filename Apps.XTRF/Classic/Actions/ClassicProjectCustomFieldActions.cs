using Apps.XTRF.Classic.Actions.Base;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Classic.Actions;

[ActionList]
public class ClassicProjectCustomFieldActions : BaseClassicCustomFieldActions
{
    public ClassicProjectCustomFieldActions(InvocationContext invocationContext) 
        : base(invocationContext, EntityType.Project)
    {
    }
    
    #region Get

    [Action("Classic: List custom fields for project", Description = "List the custom fields for a classic project, " +
                                                                     "returning fields that can be assigned to the " +
                                                                     "project without their values. To obtain the field " +
                                                                     "value, use the \"Get custom field for project\" " +
                                                                     "action corresponding to the field type")]
    public async Task<ListCustomFieldsResponse> ListCustomFieldsForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier)
        => new(await ListCustomFields(projectIdentifier.ProjectId));

    [Action("Classic: Get text or selection custom field for project",
        Description = "Retrieve a text or selection custom field for a classic project")]
    public async Task<CustomField<string>> GetTextCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetTextCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get number custom field for project", 
        Description = "Retrieve a number custom field for a classic project")]
    public async Task<CustomField<decimal?>> GetNumberCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetNumberCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get date custom field for project",
        Description = "Retrieve a date/date and time custom field for a classic project")]
    public async Task<CustomField<DateTime?>> GetDateCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetDateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get checkbox custom field for project",
        Description = "Retrieve a checkbox (boolean) custom field for a classic project")]
    public async Task<CustomBooleanField> GetCheckboxCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetCheckboxCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get multiple selection custom field for project",
        Description = "Retrieve a multiple selection (list) custom field for a classic project")]
    public async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
        => await GetMultipleSelectionCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);
    
    #endregion
    
    #region Put

    [Action("Classic: Update text or selection custom field for project",
        Description = "Update a text or selection custom field for a classic project")]
    public async Task<CustomFieldIdentifier> UpdateTextCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update number custom field for project", 
        Description = "Update a number custom field for a classic project")]
    public async Task<CustomFieldIdentifier> UpdateNumberCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] decimal value)
    {
        await UpdateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update date custom field for project",
        Description = "Update a date/date and time custom field for a classic project")]
    public async Task<CustomFieldIdentifier> UpdateDateCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        var timeZoneInfo = await GetTimeZoneInfo();
        await UpdateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, 
            new LongDateTimeRepresentation(value.ConvertToUnixTime(timeZoneInfo)));
        return customFieldIdentifier;
    }

    [Action("Classic: Update checkbox custom field for project",
        Description = "Update a checkbox (boolean) custom field for a classic project")]
    public async Task<CustomFieldIdentifier> UpdateCheckboxCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] bool value)
    {
        await UpdateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update multiple selection custom field for project",
        Description = "Update a multiple selection (list) custom field for a classic project")]
    public async Task<CustomFieldIdentifier> UpdateMultipleSelectionCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] IEnumerable<string> value)
    {
        await UpdateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }
    
    #endregion
}