using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Shared.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using XTRF.Base;

namespace Tests.XTRF
{
    [TestClass]
    public class CustomerActionTests :TestBase
    {
        [TestMethod]
        public async Task CustomerDetails_NoInput_ThrowsMisconfiguredException()
        {
            var customerActions = new CustomerActions(InvocationContext);

            await Assert.ThrowsExceptionAsync<PluginMisconfigurationException>(async () => await customerActions.GetCustomer(new Apps.XTRF.Shared.Models.Identifiers.CustomerIdentifier() { CustomerId = " " }));
        }
    }
}
