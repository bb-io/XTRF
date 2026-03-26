using Apps.XTRF.Classic.Actions.Base;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Shared.Actions;

[ActionList("Contact person custom fields")]
public class ContactPersonCustomFieldActions(InvocationContext invocationContext) 
    : BaseClassicCustomFieldActions(invocationContext, EntityType.Person)
{
    [Action("List custom fields for a contact person", Description = "List the custom fields for a contact person")]
    public async Task<ListCustomFieldsResponse> ListCustomFieldsForPerson([ActionParameter] PersonIdentifier personIdentifier)
    {
        return new(await ListCustomFields(personIdentifier.PersonId));
    }

    [Action("Get text or selection custom field for a contact person", 
        Description = "Retrieve a text or selection custom field for a contact person")]
    public async Task<CustomField<string>> GetTextCustomFieldForPerson(
        [ActionParameter] PersonIdentifier personIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier)
    {
        return await GetTextCustomField(personIdentifier.PersonId, customFieldIdentifier.Key);
    }

    [Action("Update text or selection custom field for a contact person",
        Description = "Update a text or selection custom field for a contact person")]
    public async Task<CustomFieldIdentifier> UpdateTextCustomFieldForPerson(
        [ActionParameter] PersonIdentifier personIdentifier,
        [ActionParameter] CustomFieldIdentifier customFieldIdentifier,
        [ActionParameter][Display("Value")] string value)
    {
        await UpdateCustomField(personIdentifier.PersonId, customFieldIdentifier.Key, value);
        return customFieldIdentifier;
    }
}
