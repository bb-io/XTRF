﻿using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Classic.Models.Requests.ClassicProject;
using XTRF.Base;

namespace Tests.XTRF
{
    [TestClass]
    public class ClassicProjectTests : TestBase
    {
        [TestMethod]
        public async Task CreateProject_ReturnsResponse()
        {
            var client = new ClassicProjectActions(InvocationContext,FileManager);

            var input = new CreateProjectRequest
            {
                Name = "Test",
                CustomerId="6",
                ServiceId="1",
                SpecializationId="5",
                SourceLanguageId="71",
                TargetLanguages = ["193"]
            };

            var result = await client.CreateProject(input);

            Assert.IsNotNull(result);
        }


    }
}
