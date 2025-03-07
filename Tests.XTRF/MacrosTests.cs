using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Models.Requests.Macros;
using XTRF.Base;

namespace Tests.XTRF
{
    [TestClass]
    public class MacrosTests : TestBase
    {
        [TestMethod]
        public async Task RunMacros_IsSucces()
        {
            var action = new ProviderActions(InvocationContext);

            var response = await action.RunMacroAsync(new RunMacroRequest
            {
                MacroId = "1456546",
                //ItemIds = new List<string> { "1", "2" }
            });

            Console.WriteLine(response.StatusUrl);
            Assert.IsNotNull(response);
        }
    }
}
