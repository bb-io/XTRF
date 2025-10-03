using Apps.XTRF.Classic.Models;
using Apps.XTRF.Shared.Polling;
using Apps.XTRF.Shared.Polling.Models;
using Apps.XTRF.Shared.Webhooks;
using Apps.XTRF.Shared.Webhooks.Models.Request;
using Blackbird.Applications.Sdk.Common.Polling;
using Blackbird.Applications.Sdk.Common.Webhooks;
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
    public class WebhookTests : TestBase
    {
        [TestMethod]
        public async Task On_job_status_changed_works()
        {
            var webhooks = new WebhookList(InvocationContext);
            //BILL_CREATED

            var response = await webhooks.JobStatusChangedHandler(
                new WebhookRequest { Body = "{\r\n  \"project id\": \"2025/19\",\r\n  \"project internal id\": \"DS4UTREPXJEVVOXOOU6JJAUPNA\",\r\n  \"task id\": \"2025/19/None/1\",\r\n  \"task internal id\": \"507\",\r\n  \"job id\": \"2025/19/#1/0\",\r\n  \"job internal id\": \"XYSVL2AIHJET7HYNLWINI3NW4A\",\r\n  \"status\": \"Accepted\",\r\n  \"previous status\": \"Open\",\r\n  \"job type\": \"translation\",\r\n  \"vendor\": \"TEST translator\",\r\n  \"start date and time\": \"2025-05-14T14:39:00+02:00\",\r\n  \"deadline\": \"2025-05-23T14:39:00+02:00\",\r\n  \"language combination\": {\r\n    \"sourceLanguageId\": null,\r\n    \"targetLanguageId\": null\r\n  },\r\n  \"external system in task\": \"none\"\r\n}" }, 
                null, 
                new ProjectOptionalRequest { ProjectId = "DS4UTREPXJEVVOXOOU6JJAUPNA" }, 
                new TaskOptionalRequest { }, 
                new JobOptionalRequest { JobTypeName = "translation", JobId = "XYSVL2AIHJET7HYNLWINI3NW4A" },
                new CustomerOptionalRequest { });

            Assert.IsNotNull(response.Result);
            Console.WriteLine(JsonConvert.SerializeObject(response.Result, Formatting.Indented));
        }
    }
}
