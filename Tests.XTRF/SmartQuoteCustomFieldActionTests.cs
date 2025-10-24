using XTRF.Base;
using Apps.XTRF.Smart.Actions;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Tests.XTRF;

[TestClass]
public class SmartQuoteCustomFieldActionTests : TestBase
{
    [TestMethod]
    public async Task ListCustomFieldsForQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var actions = new SmartQuoteCustomFieldActions(InvocationContext);
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };

        // Act
        var response = await actions.ListCustomFieldsForQuote(quote);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetMultipleSelectionCustomFieldForQuote_ValidInput_ReturnsResult()
    {
		// Arrange
		var actions = new SmartQuoteCustomFieldActions(InvocationContext);
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        var field = new CustomFieldIdentifier { Key = "Multiple selection custom field" };

        // Act
        var response = await actions.GetMultipleSelectionCustomFieldForQuote(quote, field);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetMultipleSelectionCustomFieldForQuote_InvalidInput_ThrowsMiscongigException()
    {
        // Arrange
        var actions = new SmartQuoteCustomFieldActions(InvocationContext);
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        var field = new CustomFieldIdentifier { Key = "Checkbox custom field" };

        // Act
        var ex = await Assert.ThrowsExceptionAsync<PluginMisconfigurationException>(
            async () => await actions.GetMultipleSelectionCustomFieldForQuote(quote, field)
        );

        // Assert
        StringAssert.Contains(ex.Message, "is not a multi_selection custom field");
    }

    [TestMethod]
    public async Task GetTextCustomFieldForQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var actions = new SmartQuoteCustomFieldActions(InvocationContext);
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        var field = new CustomFieldIdentifier { Key = "Text custom field" };

        // Act
        var response = await actions.GetTextCustomFieldForQuote(quote, field);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetNumberCustomFieldForQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var actions = new SmartQuoteCustomFieldActions(InvocationContext);
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        var field = new CustomFieldIdentifier { Key = "Number custom field" };

        // Act
        var response = await actions.GetNumberCustomFieldForQuote(quote, field);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetDateCustomFieldForQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var actions = new SmartQuoteCustomFieldActions(InvocationContext);
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        var field = new CustomFieldIdentifier { Key = "DateTime custom field" };

        // Act
        var response = await actions.GetDateCustomFieldForQuote(quote, field);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetCheckboxCustomFieldForQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var actions = new SmartQuoteCustomFieldActions(InvocationContext);
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        var field = new CustomFieldIdentifier { Key = "Checkbox custom field" };

        // Act
        var response = await actions.GetCheckboxCustomFieldForQuote(quote, field);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }
}
