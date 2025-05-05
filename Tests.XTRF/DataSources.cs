using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.DataSourceHandlers;
using Apps.XTRF.Smart.Models.Requests.File;
using Blackbird.Applications.Sdk.Common.Dynamic;
using XTRF.Base;

namespace Tests.XTRF
{
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
        public async Task JobTypesReturnsValues()
        {
            var dataHandler = new JobTypeDataHandler(InvocationContext, new CreateReceivableRequest { ProjectId="FHY653W2VBHGDLNNZR4EKNS2WU" }//3IHHDXYWDVEPDM52ONIB7SP2T4 
                );

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
    }
}
