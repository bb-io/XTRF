using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Requests.ClassicQuote;
using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Invocables;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Classic.DataSourceHandlers;

public class ClassicPriceProfileDataSource(InvocationContext invocationContext, [ActionParameter] QuoteCreateRequest request)
    : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.PersonId))
        {
            throw new Exception("You must provide a Person ID first.");
        }
        
        var customerActions = new CustomerActions(InvocationContext);
        var contactPerson = await customerActions.GetContactPerson(request);
        var tokenResponse = await GetPersonAccessToken(contactPerson?.Contact?.Emails?.Primary ?? throw new Exception("Contact person has no primary email."));
        var customerPortalClient = GetCustomerPortalClient(tokenResponse.Token);    
        
        var quoteActions = new ClassicQuoteActions(invocationContext, null!);
        FullOfficeDto officeDto;
        if (string.IsNullOrEmpty(request.OfficeId))
        {
            officeDto = await quoteActions.GetDefaultOffice(contactPerson.Contact.Emails.Primary);
        }
        else
        {
            officeDto = await quoteActions.GetOfficeById(request.OfficeId, customerPortalClient);
        }

        return officeDto.PriceProfiles
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id.ToString(), x => x.Name);
    }
}