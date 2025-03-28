﻿using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Smart.DataSourceHandlers;
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
    }
}
