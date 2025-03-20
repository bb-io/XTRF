using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Browser;
using Apps.XTRF.Shared.Models.Responses.Browser;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers
{
    public class ColumnNameDataHandler : XtrfInvocable, IAsyncDataSourceHandler
    {
        private readonly GetViewValuesRequest _input;
        public ColumnNameDataHandler(InvocationContext invocationContext, [ActionParameter] GetViewValuesRequest input)
            : base(invocationContext)
        {
            _input = input;
        }
        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var xtrfRequest = new XtrfRequest($"/browser?viewId={_input.ViewId}", Method.Get, Creds);
            var result = await Client.ExecuteWithErrorHandling<GetViewValuesDto>(xtrfRequest);

            if (result.Header?.Columns == null)
                return new Dictionary<string, string>();

            var dict = result.Header.Columns
                .ToDictionary(col => col.Name, col => col.Header);

            return dict;
        }
    }
}
