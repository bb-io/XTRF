using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Project;
using Apps.XTRF.Shared.Polling.Models;
using Apps.XTRF.Shared.Services;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.XTRF.Shared.Polling;

[PollingEventList("Views")]
public class ViewPollingList(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
{
    [PollingEvent(
        "On new record in view",
        "Triggered when a new record appears in the specified view."),
     MultipleEvents]
    public async Task<PollingEventResponse<ViewRowsMemory, List<ViewRowResponse>>> OnNewRecordInView(
        PollingEventRequest<ViewRowsMemory> request,
        [PollingEventParameter] ViewSearchRequest input)
    {
        var view = await new ViewRowsService(Client, Creds).SearchAsync(input.QueryUrl);
        return ViewPollingResponseFactory.Create(view, request.Memory);
    }
}
