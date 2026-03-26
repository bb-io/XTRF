using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Models.Identifiers;
using XTRF.Base;

namespace Tests.XTRF;

[TestClass]
public class ContactPersonCustomFieldTests : TestBase
{
	private readonly ContactPersonCustomFieldActions _actions;

	public ContactPersonCustomFieldTests() => _actions = new ContactPersonCustomFieldActions(InvocationContext);

    [TestMethod]
    public async Task ListCustomFieldsForPerson_ReturnsCustomFields()
    {
        // Arrange
        var person = new PersonIdentifier { PersonId = "27" };

        // Act
        var result = await _actions.ListCustomFieldsForPerson(person);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GetTextCustomFieldForPerson_ReturnsCustomField()
    {
        // Arrange
        var person = new PersonIdentifier { PersonId = "27" };
        var customField = new CustomFieldIdentifier { Key = "test text for personen" };

        // Act
        var result = await _actions.GetTextCustomFieldForPerson(person, customField);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateTextCustomFieldForPerson_ReturnsUpdatedCustomField()
    {
        // Arrange
        var person = new PersonIdentifier { PersonId = "27" };
        var customField = new CustomFieldIdentifier { Key = "test text for personen" };
        string value = "testfromtests";

        // Act
        var result = await _actions.UpdateTextCustomFieldForPerson(person, customField, value);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}
