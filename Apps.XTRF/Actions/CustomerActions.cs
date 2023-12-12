using Apps.XTRF.Api;
using Apps.XTRF.Constants;
using Apps.XTRF.Extensions;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Identifiers;
using Apps.XTRF.Models.Requests.Customer;
using Apps.XTRF.Models.Requests.CustomField;
using Apps.XTRF.Models.Responses.Customer;
using Apps.XTRF.Models.Responses.CustomField;
using Apps.XTRF.Models.Responses.Entities;
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
        [ActionParameter] [Display("Updated since")] DateTime? updatedSince)
    {
        var endpoint = "/customers";
        if (updatedSince is not null)
        {
            var elapsedTimeInMilliseconds = updatedSince.Value.ConvertToUnixTime();
            endpoint += $"?updatedSince={elapsedTimeInMilliseconds}";
        }

        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return new()
        {
            SimpleCustomers = await Client.ExecuteWithErrorHandling<List<SimpleCustomer>>(request)
        };
    }

    [Action("Get customer details", Description = "Get all information of a specific customer")]
    public Task<Customer> GetCustomer([ActionParameter] CustomerIdentifier customer)
    {
        var endpoint = $"/customers/{customer.CustomerId}?embed=persons";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return Client.ExecuteWithErrorHandling<Customer>(request);
    }

    [Action("Create customer", Description = "Create a new customer")]
    public Task<SimpleCustomer> CreateCustomer([ActionParameter] CreateCustomerInput input)
    {
        var request = new XtrfRequest("/customers", Method.Post, Creds)
            .WithJsonBody(new
            {
                name = input.Name,
                fullName = input.FullName,
                contact = new
                {
                    emails = new
                    {
                        primary = input.Email
                    }
                }
            }, JsonConfig.Settings);

        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Update customer", Description = "Update specific customer")]
    public Task<SimpleCustomer> UpdateCustomer([ActionParameter] CustomerIdentifier customer,
        [ActionParameter] UpdateCustomerInput input)
    {
        var endpoint = $"/customers/{customer.CustomerId}";
        var request = new XtrfRequest(endpoint, Method.Put, Creds)
            .WithJsonBody(new
            {
                id = long.Parse(customer.CustomerId),
                name = input.Name,
                fullName = input.FullName,
                contact = !string.IsNullOrEmpty(input.Email)
                    ? new
                    {
                        emails = new
                        {
                            primary = input.Email
                        }
                    }
                    : default
            }, JsonConfig.Settings);

        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Delete customer", Description = "Delete specific customer")]
    public Task DeleteCustomer([ActionParameter] CustomerIdentifier customer)
    {
        var endpoint = $"/customers/{customer.CustomerId}";
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);
        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Create customer contact", Description = "Create a new contact person for a customer")]
    public Task<SimpleCustomer> CreateCustomerContact([ActionParameter] CustomerIdentifier customer,
        [ActionParameter] CreateContactInput input)
    {
        var request = new XtrfRequest("/customers/persons", Method.Post, Creds);
        request.AddJsonBody(new
        {
            name = input.Name,
            lastName = input.LastName,
            contact = new
            {
                emails = new
                {
                    primary = input.Email
                }
            },
            customerId = customer.CustomerId
        });
        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Set contact phone number", Description = "Sets a new phone number for the contact")]
    public Task SetContactPhoneNumber([ActionParameter] PersonIdentifier person, 
        [ActionParameter] [Display("Phone number")] string phoneNumber)
    {
        var request = new XtrfRequest($"/customers/persons/{person.PersonId}/contact", Method.Put, Creds);
        request.AddJsonBody(new
        {
            phones = new List<string> { phoneNumber }
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("List customer custom fields", Description = "List custom fields of a specific customer")]
    public async Task<ListCustomFieldsResponse> ListCustomerCustomFields([ActionParameter] CustomerIdentifier customer)
    {
        var endpoint = $"/customers/{customer.CustomerId}/customFields";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<CustomFieldEntity[]>(request);
        return new(response);
    }

    [Action("Update customer custom field", Description = "Update custom field of a specific customer")]
    public Task UpdateCustomerCustomField([ActionParameter] CustomerIdentifier customer,
        [ActionParameter] UpdateCustomFieldInput input)
    {
        var endpoint = $"/customers/{customer.CustomerId}/customFields/{input.Key}";
        var request = new XtrfRequest(endpoint, Method.Put, Creds)
            .WithJsonBody(new { value = input.Value }, JsonConfig.Settings);
        return Client.ExecuteWithErrorHandling(request);
    }
}