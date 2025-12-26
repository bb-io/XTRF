using XTRF.Base;
using Newtonsoft.Json;
using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Classic.Models.Requests.ClassicProject;
using Apps.XTRF.Shared.Models.Identifiers;

namespace Tests.XTRF;

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

    [TestMethod]
    public async Task CreateLanguageCombinationForProject_ReturnsResponse()
    {
        var client = new ClassicProjectActions(InvocationContext, FileManager);

        var input = new Apps.XTRF.Classic.Models.Identifiers.LanguageCombinationIdentifier
        {
            SourceLanguageId = "74",
            TargetLanguageId = "98" 
        };

        var result = await client.CreateLanguageCombinationForProject(new Apps.XTRF.Shared.Models.Identifiers.ProjectIdentifier { ProjectId= "40877" }
        ,input);

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task Downloadfile_ReturnsResponse()
    {
        var client = new ClassicProjectActions(InvocationContext, FileManager);

        var result = await client.DownloadFile(new Apps.XTRF.Classic.Models.Identifiers.ClassicTaskIdentifier { TaskId= "312425" },
            new Apps.XTRF.Classic.Models.Identifiers.ClassicFileIdentifier { FileId= "1808308" });

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CreateReceivableForProject_ReturnsResponse()
    {
        // Arrange
        var actions = new ClassicProjectActions(InvocationContext, FileManager);
        var project = new ProjectIdentifier { ProjectId = "77" };
        var request = new CreateReceivableClassicRequest
        {
            TaskId = "139",
            JobType = "1",
            CalculationUnitId = "1",
            Type = "SIMPLE",
            Rate = "1",
            Quantity = 1,
            File = new Blackbird.Applications.Sdk.Common.Files.FileReference { Name = "test.html" }
        };

        // Act
        var result = await actions.CreateReceivableForProject(project, request);

        // Assert
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Assert.IsNotNull(result);
    }
}
