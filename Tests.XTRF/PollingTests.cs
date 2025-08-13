using Apps.XTRF.Shared.Polling;
using Apps.XTRF.Shared.Polling.Models;
using Apps.XTRF.Shared.Webhooks;
using Blackbird.Applications.Sdk.Common.Polling;
using Newtonsoft.Json;
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
        public async Task OnVendorInvoicesStatusChanged_IsSuccess()
        {
            var polling = new InvoicePollingList(InvocationContext);
            //BILL_CREATED

            var initialMemory = new StatusMemory
            {
                StatusMap = new Dictionary<string, string>()
            };
            var input = new VendorInvoiceStatusChangedInput
            {
                Status = "SENT",
                InvoiceId = null  
            };

            var request = new PollingEventRequest<StatusMemory>
            {
                Memory = initialMemory,
                PollingTime = DateTime.UtcNow.AddDays(-1)
            };

            var response = await polling.OnVendorInvoicesStatusChanged(request, input);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task OnClientInvoicesStatusChanged_IsSuccess()
        {
            var polling = new InvoicePollingList(InvocationContext);

            var input = new InvoiceStatusChangedInput
            {
                Status = "SENT",
            };

            var request = new PollingEventRequest<StatusMemory>
            {
                //Memory = new StatusMemory
                //{
                //    StatusMap = new Dictionary<string, string>
                //    {
                //        { "64771", "READY" },
                //        { "64769", "SENT" }
                //    },
                //    LastUpdatedTime = DateTime.UtcNow
                //}
                Memory = null
            };

            var response = await polling.OnClientInvoicesStatusChanged(request, input);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(response);
        }
    }
}
