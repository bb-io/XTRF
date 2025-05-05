using Apps.XTRF.Shared.Polling;
using Apps.XTRF.Shared.Polling.Models;
using Apps.XTRF.Shared.Webhooks;
using Blackbird.Applications.Sdk.Common.Polling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRF.Base;

namespace Tests.XTRF
{
    [TestClass]
    public class PollingTests : TestBase
    {
        [TestMethod]
        public async Task TestClientInvoicesCreated()
        {
            var polling = new InvoicePollingList(InvocationContext);
            //BILL_CREATED

            var initialMemory = new StatusMemory
            {
                StatusMap = new Dictionary<string, string>()
            };
            var input = new VendorInvoiceStatusChangedInput
            {
                Status = "BILL_CREATED",
                InvoiceId = null  
            };

            var request = new PollingEventRequest<StatusMemory>
            {
                Memory = initialMemory,
                PollingTime = DateTime.UtcNow
            };

            var response = await polling.OnVendorInvoicesStatusChanged(request, input);

            Assert.IsNotNull(response);
        }
    }
}
