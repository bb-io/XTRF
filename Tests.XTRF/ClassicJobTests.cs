using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Classic.Models.Requests.ClassicJob;
using Apps.XTRF.Shared.Models.Identifiers;
using Newtonsoft.Json;
using XTRF.Base;

namespace Tests.XTRF;

[TestClass]
public class ClassicJobTests : TestBase
{
    [TestMethod]
    public async Task GetJob_CorrectRequest_ReturnsJob()
    {
        // Arrange
        var actions = new ClassicJobActions(InvocationContext, FileManager);
        var identifier = new JobIdentifier { JobId = "177" };

        // Act
        var result = await actions.GetJob(identifier);

        // Assert
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task Update_CorrectRequest_ReturnsJob()
    {
        // Arrange
        var actions = new ClassicJobActions(InvocationContext, FileManager);
        var identifier = new JobIdentifier { JobId = "177" };
        var request = new UpdateJobRequest { Status = "STARTED" };

        // Act
        var result = await actions.UpdateJob(identifier, request);

        // Assert
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Assert.IsNotNull(result);
    }
}
