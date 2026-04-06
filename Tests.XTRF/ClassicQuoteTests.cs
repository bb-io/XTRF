using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Classic.Models.Requests.ClassicQuote;
using XTRF.Base;

namespace Tests.XTRF;

[TestClass]
public class ClassicQuoteTests : TestBase
{
    [TestMethod]
    public async Task GetQuote_ValidInput_ReturnsResult()
    {
        var action = new ClassicQuoteActions(InvocationContext, FileManager);
        var response = await action.GetQuote(new Apps.XTRF.Shared.Models.Identifiers.QuoteIdentifier()
        {
            QuoteId = "fakeId"
        });

        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task CreateQuote_ReturnsResult()
    {
        var action = new ClassicQuoteActions(InvocationContext, FileManager);
        var response = await action.CreateQuote(new QuoteCreateRequest()
        {
            Note= "Quote generated automatically from Hubspot on 2026-04-04T18:28:24.264Z",
            PersonId = "60015",
            QuoteName = "Devis TERAGIR - ALIX",
            ServiceId = "30",
            PriceProfileId = "33920",
            SourceLanguageId = "87",
            SpecializationId = "154",
        });

        Assert.IsNotNull(response);
    }
}
