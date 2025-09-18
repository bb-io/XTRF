using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Models.Requests.Browser;
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
}
