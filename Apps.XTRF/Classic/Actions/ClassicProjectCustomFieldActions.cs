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
        => await ListCustomFields(projectIdentifier.ProjectId);

    [Action("Classic: Get text or selection custom field for project",
        Description = "Retrieve a text or selection custom field for a classic project")]
    public async Task<CustomField<string>> GetTextCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetTextCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get number custom field for project", 
        Description = "Retrieve a number custom field for a classic project")]
    public async Task<CustomField<decimal?>> GetNumberCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetNumberCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get date custom field for project",
        Description = "Retrieve a date/date and time custom field for a classic project")]
    public async Task<CustomField<DateTime?>> GetDateCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetDateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get checkbox custom field for project",
        Description = "Retrieve a checkbox (boolean) custom field for a classic project")]
    public async Task<CustomField<bool?>> GetCheckboxCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetCheckboxCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);

    [Action("Classic: Get multiple selection custom field for project",
        Description = "Retrieve a multiple selection (list) custom field for a classic project")]
    public async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier)
        => await GetMultipleSelectionCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key);
    
    #endregion
    
    #region Put

    [Action("Classic: Update text custom field for project",
        Description = "Update a text custom field for a classic project")]
    public async Task<ClassicCustomFieldIdentifier> UpdateTextCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateTextCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, customFieldIdentifier.Name,
            value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update number custom field for project", 
        Description = "Update a number custom field for a classic project")]
    public async Task<ClassicCustomFieldIdentifier> UpdateNumberCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] decimal value)
    {
        await UpdateNumberCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, customFieldIdentifier.Name,
            value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update date custom field for project",
        Description = "Update a date custom field for a classic project")]
    public async Task<ClassicCustomFieldIdentifier> UpdateDateCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        await UpdateDateCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, customFieldIdentifier.Name,
            value);
        return customFieldIdentifier;
    }
    
    [Action("Classic: Update date and time custom field for project",
        Description = "Update a date and time custom field for a classic project")]
    public async Task<ClassicCustomFieldIdentifier> UpdateDateTimeCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] DateTime value)
    {
        await UpdateDateTimeCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key,
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }

    [Action("Classic: Update checkbox custom field for project",
        Description = "Update a checkbox (boolean) custom field for a classic project")]
    public async Task<ClassicCustomFieldIdentifier> UpdateCheckboxCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] bool value)
    {
        await UpdateCheckboxCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key,
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }
    
    [Action("Classic: Update selection custom field for project",
        Description = "Update a selection custom field for a classic project")]
    public async Task<ClassicCustomFieldIdentifier> UpdateSelectionCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] string value)
    {
        await UpdateSelectionCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, 
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }
    
    [Action("Classic: Update multiple selection custom field for project",
        Description = "Update a multiple selection (list) custom field for a classic project")]
    public async Task<ClassicCustomFieldIdentifier> UpdateMultipleSelectionCustomFieldForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicCustomFieldIdentifier customFieldIdentifier,
        [ActionParameter] [Display("Value")] IEnumerable<string> value)
    {
        await UpdateMultipleSelectionCustomField(projectIdentifier.ProjectId, customFieldIdentifier.Key, 
            customFieldIdentifier.Name, value);
        return customFieldIdentifier;
    }
    
    #endregion
}