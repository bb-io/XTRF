using XTRF.Base;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Actions;
using Apps.XTRF.Smart.Models.Requests.SmartQuote;
using Apps.XTRF.Classic.Models.Requests.ClassicQuote;

namespace Tests.XTRF;

[TestClass]
public class SmartQuoteActionTests : TestBase
{
    [TestMethod]
    public async Task GetSmartQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var action = new SmartQuoteActions(InvocationContext, FileManager);
        var quote = new QuoteIdentifier() { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        
        // Act
        var response = await action.GetQuote(quote, false);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task AddReceivableToQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var action = new SmartQuoteActions(InvocationContext, FileManager);
        var quote = new QuoteIdentifier { QuoteId = "K7NPUOBE2VAA3A2SDFYBZ2BK3I" };
        var input = new AddQuoteReceivableRequest
        {
            CalculationUnitId = "1",
            Units = 1,
            IsLanguageIndependent = true
        };

        // Act
        var response = await action.AddReceivableToQuote(quote, input);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task UpdateQuote_ValidInput_ReturnsResult()
    {
        // Arrange
        var action = new SmartQuoteActions(InvocationContext, FileManager);
        var id = new QuoteIdentifier { QuoteId = "B2L3KJDBQZDRHBIDHF6J65XJ2I" };
        var input = new UpdateQuoteRequest { };

        // Act
        var response = await action.UpdateQuote(id, input);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetFilesByProject_ValidInput_ReturnsResult()
    {
        // Arrange
        var action = new SmartProjectActions(InvocationContext, FileManager);
        var id = new ProjectIdentifier { ProjectId = "SRNSNDLZWFBSVGUNRYXI2IYD4U" };
        var input = new Apps.XTRF.Smart.Models.Identifiers.FilterLanguageOptionalIdentifier { };
        var categoryInput = new Apps.XTRF.Smart.Models.Identifiers.SmartFileCategoryOptionalIdentifier { Category = "SOURCE_DOCUMENT" };
        // Act
        var response = await action.GetFilesByProject(id, input, categoryInput);

        // Assert
        PrintJsonResult(response);
        Assert.IsNotNull(response);
    }
}
