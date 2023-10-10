using Apps.XTRF.Api;
using Apps.XTRF.Constants;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.InputParameters;
using Apps.XTRF.Models.Requests.ManageCustomer;
using Apps.XTRF.Models.Responses;
using Apps.XTRF.Models.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Actions;

[ActionList]
public class CustomerActions : XtrfInvocable
{
    public CustomerActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get customers", Description = "Get all customers on this XTRF instance")]
    public async Task<GetCustomersResponse> GetCustomers(
        [ActionParameter] [Display("Updated since timestamp")] int? updatedSince)
    {
        var endpoint = "/customers";
        if (updatedSince is not null)
            endpoint += $"?updatedSince={updatedSince}";

        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return new()
        {
            SimpleCustomers = await Client.ExecuteWithErrorHandling<List<SimpleCustomer>>(request)
        };
    }

    [Action("Get customer details", Description = "Get all information of a specific customer")]
    public Task<Customer> GetCustomer([ActionParameter] CustomerRequest customer,
        [ActionParameter] [Display("Additional fields")]
        string? embed)
    {
        var endpoint = "/customers/" + customer.CustomerId;
        if (embed is not null)
            endpoint += $"?embed={embed}";

        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return Client.ExecuteWithErrorHandling<Customer>(request);
    }

    [Action("Create customer", Description = "Create a new customer")]
    public Task<SimpleCustomer> CreateCustomer([ActionParameter] CreateCustomer newCustomer)
    {
        var request = new XtrfRequest("/customers", Method.Post, Creds)
            .WithJsonBody(new CreateCustomerRequest(newCustomer), JsonConfig.Settings);

        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Update customer", Description = "Update specific customer")]
    public Task<SimpleCustomer> UpdateCustomer([ActionParameter] UpdateCustomer input)
    {
        var endpoint = $"/customers/{input.CustomerId}";
        var request = new XtrfRequest(endpoint, Method.Put, Creds)
            .WithJsonBody(new UpdateCustomerRequest(input), JsonConfig.Settings);

        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Delete customer", Description = "Delete specific customer")]
    public Task DeleteCustomer([ActionParameter] CustomerRequest customer)
    {
        var endpoint = $"/customers/{customer.CustomerId}";
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Create customer contact", Description = "Create a new contact person for a customer")]
    public Task<SimpleCustomer> CreateCustomerContact([ActionParameter] CreateContact newContact)
    {
        if (!int.TryParse(newContact.CustomerId, out var customerId))
            throw new("Customer ID value must be a number");

        var request = new XtrfRequest("/customers/persons", Method.Post, Creds);
        request.AddJsonBody(new
        {
            name = newContact.Name,
            lastName = newContact.LastName,
            contact = new
            {
                emails = new
                {
                    primary = newContact.Email
                }
            },
            customerId
        });
        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Set contact phone number", Description = "Sets a new phone number for the contact")]
    public Task SetContactPhoneNumber([ActionParameter] SetPhoneNumber input)
    {
        var request = new XtrfRequest($"/customers/persons/{input.ContactId}/contact", Method.Put, Creds);
        request.AddJsonBody(new
        {
            phones = new List<string>() { input.PhoneNumber }
        });

        return Client.ExecuteWithErrorHandling(request);
    }
}