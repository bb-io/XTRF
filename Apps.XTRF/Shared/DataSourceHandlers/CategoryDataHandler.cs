using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class CategoryDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    private record Category(string Id, string Name);

    public CategoryDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new XtrfRequest("/dictionaries/category/active", Method.Get, Creds);
        var categories = await Client.ExecuteWithErrorHandling<IEnumerable<Category>>(request);

        return categories
            .Where(category => context.SearchString == null
                               || category.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(category => category.Id, category => category.Name);
    }
}

