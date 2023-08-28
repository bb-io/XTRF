using Apps.XTRF.Extensions;
using Apps.XTRF.InputParameters;
using Apps.XTRF.Requests.ManageCustomer;
using Apps.XTRF.Responses;
using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF.Actions;

[ActionList]
public class CustomerActions
{
    [Action("Get customers", Description = "Get all customers on this XTRF instance")]
    public GetCustomersResponse GetCustomers(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Updated since timestamp")] int? updatedSince)
    {
        var endpoint = "/customers";
        if (updatedSince is not null)
            endpoint += $"?updatedSince={updatedSince}";
            
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest(endpoint, Method.Get, authenticationCredentialsProviders);
        return new GetCustomersResponse()
        {
            SimpleCustomers = client.ExecuteRequest<List<SimpleCustomer>>(request)
        };
    }

    [Action("Get customer details", Description = "Get all information of a specific customer")]
    public Customer GetCustomer(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CustomerRequest customer,
        [ActionParameter] [Display("Additional fields")] string? embed)
    {
        var endpoint = "/customers/" + customer.CustomerId;
        if (embed is not null)
            endpoint += $"?embed={embed}";
            
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest(endpoint, Method.Get, authenticationCredentialsProviders);
        return client.ExecuteRequest<Customer>(request);
    }

    [Action("Create customer", Description = "Create a new customer")]
    public SimpleCustomer CreateCustomer(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateCustomer newCustomer)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/customers", Method.Post, authenticationCredentialsProviders);
        request.WithJsonBody(new CreateCustomerRequest(newCustomer));
            
        return client.ExecuteRequest<SimpleCustomer>(request);
    }
        
    [Action("Update customer", Description = "Update specific customer")]
    public SimpleCustomer UpdateCustomer(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UpdateCustomer input)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);

        var endpoint = $"/customers/{input.CustomerId}";
        var request = new XtrfRequest(endpoint, Method.Put, authenticationCredentialsProviders);
        request.WithJsonBody(new UpdateCustomerRequest(input));
            
        return client.ExecuteRequest<SimpleCustomer>(request);
    }        
        
    [Action("Delete customer", Description = "Delete specific customer")]
    public void DeleteCustomer(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CustomerRequest customer)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);

        var endpoint = $"/customers/{customer.CustomerId}";
        var request = new XtrfRequest(endpoint, Method.Delete, authenticationCredentialsProviders);
            
        client.ExecuteRequest(request);
    }

    [Action("Create customer contact", Description = "Create a new contact person for a customer")]
    public SimpleCustomer CreateCustomerContact(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateContact newContact)
    {
        if (!int.TryParse(newContact.CustomerId, out var customerId))
            throw new("Customer ID value must be a number");
                
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/customers/persons", Method.Post, authenticationCredentialsProviders);
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
        return client.ExecuteRequest<SimpleCustomer>(request);
    }

    [Action("Set contact phone number", Description = "Sets a new phone number for the contact")]
    public void SetContactPhoneNumber(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] SetPhoneNumber input)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest($"/customers/persons/{input.ContactId}/contact", Method.Put, authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            phones = new List<string>() { input.PhoneNumber }
        });
        client.ExecuteRequest(request);
    }
}