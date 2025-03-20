using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Shared.Actions;
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
            var action = new ProviderPersonActions(InvocationContext);
            var response = await action.GetViewValuesAsync( new Apps.XTRF.Shared.Models.Requests.Browser.GetViewValuesRequest { ViewId="17918"});

            Assert.IsNotNull(response);
        }
    }
}
