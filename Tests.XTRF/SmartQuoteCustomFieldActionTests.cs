using XTRF.Base;
using Apps.XTRF.Smart.Actions;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Apps.XTRF.Smart.Models.Identifiers;
using Apps.XTRF.Smart.Models.Requests.File;

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
        var field = new SmartQuoteCustomFieldIdentifier { Key = "Multiple selection custom field" };

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
        var field = new SmartQuoteCustomFieldIdentifier { Key = "Checkbox custom field" };

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
        var field = new SmartQuoteCustomFieldIdentifier { Key = "Selection custom field" };

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
        var field = new SmartQuoteCustomFieldIdentifier { Key = "Number custom field" };

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
        var field = new SmartQuoteCustomFieldIdentifier { Key = "DateTime custom field" };

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
        var field = new SmartQuoteCustomFieldIdentifier { Key = "Checkbox custom field" };

        // Act
        var response = await actions.GetCheckboxCustomFieldForQuote(quote, field);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    //CreateReceivableForProject


    [TestMethod]
    public async Task CreateReceivableForProject_ValidInput_ReturnsResult()
    {
        // Arrange
        var actions = new SmartProjectActions(InvocationContext, FileManager);
        var create = new CreateReceivableRequest { File= new Blackbird.Applications.Sdk.Common.Files.FileReference { Name= "test.txt" },
        JobType= "839",
        ProjectId= "HLMS43Q3DVERJLGE7FK66QYHWM",
            SourceLanguageId = "74",
            TargetLanguageId = "215",
            CalculationUnitId = "1",
        };

        // Act
        var response = await actions.CreateReceivableForProject(create);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }
}
