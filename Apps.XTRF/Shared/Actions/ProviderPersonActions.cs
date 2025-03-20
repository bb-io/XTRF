using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Browser;
using Apps.XTRF.Shared.Models.Requests.Provider;
using Apps.XTRF.Shared.Models.Responses.Browser;
using Apps.XTRF.Shared.Models.Responses.Provider;
using Apps.XTRF.Shared.Models.Responses.Provider.Persons;
using Apps.XTRF.Smart.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.Actions;

[ActionList]
public class ProviderPersonActions(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
{
    [Action("Search provider persons", Description = "Search for provider persons based on the given criteria")]
    public async Task<ProviderPersonSearchResponse> SearchProviderPersonsAsync([ActionParameter] ProviderPersonSearchRequest request)
    {
        var xtrfRequest = new XtrfRequest("/providers/persons/ids", Method.Get, Creds);
        var ids = await Client.ExecuteWithErrorHandling<List<int>>(xtrfRequest);
        
        var response = new ProviderPersonSearchResponse();
        foreach (var id in ids)
        {
            var providerPerson = await GetProviderPersonAsync(new ProviderPersonIdentifier
            {
                ProviderPersonId = id.ToString()
            });
            
            if(request.Email != null && !providerPerson.Contact.Emails.Primary.Equals(request.Email))
            {
                continue;
            }
            
            response.ProviderPersons.Add(providerPerson);
        }
        
        return response;
    }

    [Action("Get provider person", Description = "Get provider person by ID")]
    public async Task<ProviderPersonResponse> GetProviderPersonAsync([ActionParameter] ProviderPersonIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/providers/persons/{request.ProviderPersonId}", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ProviderPersonDto>(xtrfRequest);

        return new ProviderPersonResponse()
        {
            Id = response.Id,
            Name = response.Name,
            LastName = response.LastName,
            Contact = new() { Emails = new() { Cc = response.Contact.Emails.Cc, Additional = response.Contact.Emails.Additional, Primary = response.Contact.Emails.Primary },Fax = response.Contact.Fax, Phones = response.Contact.Phones, Sms = response.Contact.Sms, Websites = response.Contact.Websites },
            PositionId = response.PositionId,
            Gender = response.Gender,
            Active = response.Active,
            MotherTonguesIds = response.MotherTonguesIds,
            ProviderId = response.ProviderId,
        };
    }

    [Action("Remove provider person", Description = "Remove provider person by ID")]
    public async Task RemoveProviderPersonAsync([ActionParameter] ProviderPersonIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/providers/persons/{request.ProviderPersonId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(xtrfRequest);
    }
    
    [Action("Send invitation to provider person", Description = "Send provider person invitation by ID")]
    public async Task<SendInvitationResponse> SendProviderPersonInvitationAsync([ActionParameter] ProviderPersonIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/providers/persons/{request.ProviderPersonId}/notification/invitation", Method.Post, Creds);
        return await Client.ExecuteWithErrorHandling<SendInvitationResponse>(xtrfRequest);
    }



    [Action("Get view values", Description = "Retrive values by the ID of your view with a specified columns")]
    public async Task<GetViewValuesResponse> GetViewValuesAsync([ActionParameter] GetViewValuesRequest request)
    {
        var xtrfRequest = new XtrfRequest($"/browser?viewId={request.ViewId}", Method.Get, Creds);
        var result = await Client.ExecuteWithErrorHandling<GetViewValuesDto>(xtrfRequest);

        if (request.Columns != null && request.Columns.Any())
        {
            var headerColumns = result.Header.Columns.ToList();

            var selectedIndices = headerColumns
                .Select((col, index) => new { col, index })
                .Where(x => request.Columns.Contains(x.col.Name))
                .Select(x => x.index)
                .ToList();

            result.Header.Columns = headerColumns
                .Where((col, index) => selectedIndices.Contains(index))
                .ToList();

            var rowsList = result.Rows.ToList();
            foreach (var row in rowsList)
            {
                row.Columns = selectedIndices.Select(idx => row.Columns[idx]).ToList();
            }
            result.Rows = rowsList;
        }

        var response = new GetViewValuesResponse
        {
            ViewId = request.ViewId,
            Header = result.Header,
            Rows = result.Rows,

        };

        return response;
    }

}