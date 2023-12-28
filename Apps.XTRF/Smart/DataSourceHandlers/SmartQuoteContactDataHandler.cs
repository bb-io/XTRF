using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Smart.DataSourceHandlers;

public class SmartQuoteContactDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    private readonly QuoteIdentifier _quoteIdentifier;
    
    public SmartQuoteContactDataHandler(InvocationContext invocationContext, [ActionParameter] QuoteIdentifier quoteIdentifier)
        : base(invocationContext)
    {
        _quoteIdentifier = quoteIdentifier;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (_quoteIdentifier.QuoteId == null)
            throw new Exception("Please enter Quote ID first.");
        
        var getQuoteRequest = new XtrfRequest($"/v2/quotes/{_quoteIdentifier.QuoteId}", Method.Get, Creds);
        var quote = await Client.ExecuteWithErrorHandling<SmartQuote>(getQuoteRequest);
        
        var getCustomerRequest = new XtrfRequest($"/customers/{quote.ClientId}?embed=persons", Method.Get, Creds);
        var customer = await Client.ExecuteWithErrorHandling<Customer>(getCustomerRequest);
        return customer.Persons
            .Select(person => new
            {
                person.Id,
                FullName = person.Name + (string.IsNullOrWhiteSpace(person.LastName) ? "" : " " + person.LastName)
            })
            .Where(person => context.SearchString == null
                             || person.FullName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(person => person.Id, person => person.FullName);
    }
}