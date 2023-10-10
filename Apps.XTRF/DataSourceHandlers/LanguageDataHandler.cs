﻿using Apps.XTRF.Api;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Responses.Models;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.DataSourceHandlers;

public class LanguageDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
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