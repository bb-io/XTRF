using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class SpecializationDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    record Specialization(string Id, string Name);
    
    public SpecializationDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new XtrfRequest("/dictionaries/specialization/active", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<IEnumerable<Specialization>>(request);

        return response
            .Where(specialization => context.SearchString == null 
                                     || specialization.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(specialization => specialization.Id, specialization => specialization.Name);
    }
}