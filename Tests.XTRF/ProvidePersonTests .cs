using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Models.Requests.Browser;
using XTRF.Base;

namespace Tests.XTRF
{
    [TestClass]
    public class ProvidePersonTests :TestBase
    {
        [TestMethod]
        public async Task SearchProviderPersons_ValidInput_ReturnsResult()
        {
            var action = new ProviderPersonActions(InvocationContext);
            var response = await action.SearchProviderPersonsAsync(new Apps.XTRF.Shared.Models.Requests.Provider.ProviderPersonSearchRequest()
            {
                Email = "testvendor@blackbird.io"
            });

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task GetViewValues_ValidInput_ReturnsResult()
        {
            var action = new ViewActions(InvocationContext);

            var response = await action.GetViewValuesAsync(
            new GetViewValuesRequest
            {
                ViewId = "17918",
                Columns = ["internalNumber", "activitiesIdNumbers"],
                ColumnsValue = ["2024/3697", "FR2024-9-953-2/3"]
            });

                Console.WriteLine($"{response.Row.Id} - {string.Join(", ", response.Row.Columns)}");
            Assert.IsNotNull(response);
        }
    }
}
