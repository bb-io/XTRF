using Apps.XTRF.Classic.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using XTRF.Base;

namespace Tests.XTRF;

[TestClass]
public class DataSources : TestBase
{
    [TestMethod]
    public async Task DictionaryDataHandlerReturnsValues()
    {
        var dataHandler = new ProviderInvoiceDataHandler(InvocationContext);

        var result = await dataHandler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} - {item.Value}");
            Assert.IsTrue(item.Key != null);
        }
    }

    [TestMethod]
    public async Task SmartServiceDataSourceHandlerReturnsValues()
    {
        var dataHandler = new SmartServiceDataSourceHandler(InvocationContext);

        var result = await dataHandler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} - {item.Value}");
            Assert.IsTrue(item.Key != null);
        }
    }

    [TestMethod]
    public async Task ClasicPersonDataSourceHandlerReturnsValues()
    {
        var dataHandler = new ClassicPersonDataSource(InvocationContext);

        var result = await dataHandler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} - {item.Value}");
            Assert.IsTrue(item.Key != null);
        }
    }

    [TestMethod]
    public async Task JobTypesReturnsValues()
    {
        var dataHandler = new JobTypeDataHandler(InvocationContext);

        var result = await dataHandler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} - {item.Value}");
            Assert.IsTrue(item.Key != null);
        }
    }

    [TestMethod]
    public async Task JobTypesNamesReturnsValues()
    {
        var dataHandler = new JobTypeNameDataHandler(InvocationContext);

        var result = await dataHandler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} - {item.Value}");
            Assert.IsTrue(item.Key != null);
        }
    }

    [TestMethod]
    public async Task JobStatusReturnsValues()
    {
        var dataHandler = new JobStatusDataHandler(InvocationContext, new JobIdentifier { JobId = "FGMD2QMQDZA6FA3CRINMZKYF64" }
            );

        var result = await dataHandler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} - {item.Value}");
            Assert.IsTrue(item.Key != null);
        }
    }

    //LanguageDataHandler

    [TestMethod]
    public async Task LanguageDataHandlerReturnsValues()
    {
        var dataHandler = new LanguageDataHandler(InvocationContext );

        var result = await dataHandler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} - {item.Value}");
            Assert.IsTrue(item.Key != null);
        }
    }

    [TestMethod]
    public async Task SmartQuoteCustomFieldDataHandlerReturnsValues()
    {
        // Arrange
        var quote = new QuoteIdentifier { QuoteId = "AHMG2QPUCBE6XGTB7XLDZ7R4AI" };
        var handler = new SmartQuoteCustomFieldDataHandler(quote, InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        // Assert
        foreach (var item in result)
            Console.WriteLine($"{item.DisplayName}");
    }

    [TestMethod]
    public async Task ClassicQuoteCustomFieldDataHandlerReturnsValues()
    {
        // Arrange
        var quote = new QuoteIdentifier { QuoteId = "19" };
        var handler = new ClassicQuoteCustomFieldDataHandler(quote, InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

        // Assert
        foreach (var item in result)
            Console.WriteLine($"{item.DisplayName}");
    }
}
