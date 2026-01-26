using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Models.Requests.Browser;
using Apps.XTRF.Shared.Models.Requests.Project;
using XTRF.Base;

namespace Tests.XTRF;

[TestClass]
public class ViewActionsTests : TestBase
{
    private ViewActions _actions => new(InvocationContext);

    [TestMethod]
    public async Task GetFilteredViewValuesAsync_Works()
    {
        var request = new GetViewValuesRequest
        {
            ViewId = "160",
            Columns = ["customer.fullname"],
            ColumnsValue = ["Vitalii"]
        };

        var result = await _actions.GetFilteredViewValuesAsync(request);

        Assert.IsTrue(result.TotalRows > 0);
    }

    [TestMethod]
    public async Task FindProject_Works()
    {
        var request = new Apps.XTRF.Shared.Models.Requests.Project.ProjectSearchRequest
        {
            ViewId = "62",
            Text = "prj",
            //DeadlineFrom = new DateTime(2024, 7, 5),
            //SourceLanguageIsNot = ["193"],
            //TargetLanguageIsNot = ["193"]
        };

        var result = await _actions.FindProject(request);
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task SearchProjects_Works()
    {
        var request = new ProjectSearchRequest
        {
            ViewId = "62",
            //Text = "prj",
            //TargetLanguageIsNot = ["193"]
            SourceLanguageIsNot = ["193"],
            Status= ["CLOSED"]
        };

        var result = await _actions.SearchProjects(request);
        Console.WriteLine($"Total: {result.Projects.Count()}");
        foreach (var project in result.Projects) {
            Console.WriteLine($"{project.Id}");
        }
        Assert.IsTrue(result.TotalRows > 0);
    }
}
