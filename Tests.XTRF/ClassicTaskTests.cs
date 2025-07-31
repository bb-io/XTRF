using Apps.XTRF.Classic.Actions;
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
    public class ClassicTaskTests :TestBase
    {
        [TestMethod]
        public async Task GetClassicTask_IsSuccess()
        {
            var action = new ClassicTaskActions(InvocationContext,FileManager);

            var result = await action.GetTaskInProject(new Apps.XTRF.Shared.Models.Identifiers.ProjectIdentifier { ProjectId= "" },
                new Apps.XTRF.Classic.Models.Identifiers.ClassicTaskIdentifier { TaskId= "" });

            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(result);
        }
    }
}
