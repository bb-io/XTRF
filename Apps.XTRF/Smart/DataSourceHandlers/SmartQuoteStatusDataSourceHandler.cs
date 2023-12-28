using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Smart.DataSourceHandlers;

public class SmartQuoteStatusDataSourceHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    private readonly QuoteIdentifier _quoteIdentifier;
    
    public SmartQuoteStatusDataSourceHandler(InvocationContext invocationContext, [ActionParameter] QuoteIdentifier quoteIdentifier)
        : base(invocationContext)
    {
        _quoteIdentifier = quoteIdentifier;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var availableStatuses = new Dictionary<string, string>
        {
            { "PENDING", "Pending" },
            { "SENT", "Sent" },
            { "APPROVED", "Approved" },
            { "REJECTED", "Rejected" }
        };

        if (_quoteIdentifier.QuoteId == null)
            return availableStatuses;

        var getQuoteRequest = new XtrfRequest($"/v2/quotes/{_quoteIdentifier.QuoteId}", Method.Get, Creds);
        var quote = await Client.ExecuteWithErrorHandling<SmartQuote>(getQuoteRequest);
        
        switch (quote.Status)
        {
            case "PENDING":
                return new()
                {
                    { "SENT", "Sent" },
                    { "APPROVED", "Approved" },
                    { "REJECTED", "Rejected" }
                };
            case "SENT":
                return new()
                {
                    { "APPROVED", "Approved" },
                    { "REJECTED", "Rejected" }
                };
            case "APPROVED":
                return new();
            case "REJECTED":
                return new()
                {
                    { "PENDING", "Pending" }
                };
            case "REQUESTED":
                return new()
                {
                    { "PENDING", "Pending" },
                    { "APPROVED", "Approved" },
                    { "REJECTED", "Rejected" }
                };
            case "APPROVED_BY_CLIENT": 
                return new()
                {
                    { "APPROVED", "Approved" }
                };
            default:
                return availableStatuses;
        }
    }
}