using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class LanguageDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    record Language(string Id, string Name);

    public LanguageDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new XtrfRequest("/dictionaries/language/active", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<List<Language>>(request);

        return response
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}