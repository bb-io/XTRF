using Apps.XTRF.Api;
using Apps.XTRF.Constants;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Requests.Customer;
using Apps.XTRF.Models.Requests.CustomField;
using Apps.XTRF.Models.Requests.ManageCustomer;
using Apps.XTRF.Models.Responses;
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
        [ActionParameter] [Display("Updated since timestamp")]
        int? updatedSince)
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
    public Task<SimpleCustomer> CreateCustomer([ActionParameter] CreateCustomerInput newCustomerInput)
    {
        var request = new XtrfRequest("/customers", Method.Post, Creds)
            .WithJsonBody(new CreateCustomerRequest(newCustomerInput), JsonConfig.Settings);

        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Update customer", Description = "Update specific customer")]
    public Task<SimpleCustomer> UpdateCustomer([ActionParameter] UpdateCustomerInput input)
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
    public Task<SimpleCustomer> CreateCustomerContact([ActionParameter] CreateContactInput newContactInput)
    {
        if (!int.TryParse(newContactInput.CustomerId, out var customerId))
            throw new("Customer ID value must be a number");

        var request = new XtrfRequest("/customers/persons", Method.Post, Creds);
        request.AddJsonBody(new
        {
            name = newContactInput.Name,
            lastName = newContactInput.LastName,
            contact = new
            {
                emails = new
                {
                    primary = newContactInput.Email
                }
            },
            customerId
        });
        return Client.ExecuteWithErrorHandling<SimpleCustomer>(request);
    }

    [Action("Set contact phone number", Description = "Sets a new phone number for the contact")]
    public Task SetContactPhoneNumber([ActionParameter] SetPhoneNumberInput input)
    {
        var request = new XtrfRequest($"/customers/persons/{input.ContactId}/contact", Method.Put, Creds);
        request.AddJsonBody(new
        {
            phones = new List<string>() { input.PhoneNumber }
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("List customer custom fields", Description = "List custom fields of a specific customer")]
    public async Task<ListCustomFieldsResponse> ListCustomerCustomFields([ActionParameter] CustomerRequest customer)
    {
        var endpoint = $"/customers/{customer.CustomerId}/customFields";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        var response = await Client.ExecuteWithErrorHandling<CustomFieldEntity[]>(request);
        return new(response);
    }

    [Action("Update customer custom field", Description = "Update custom field of a specific customer")]
    public Task UpdateCustomerCustomField([ActionParameter] CustomerRequest customer,
        [ActionParameter] UpdateCustomFieldInput input)
    {
        var endpoint = $"/customers/{customer.CustomerId}/customFields/{input.Key}";
        var request = new XtrfRequest(endpoint, Method.Put, Creds)
            .WithJsonBody(new UpdateCustomFieldRequest(input.Value), JsonConfig.Settings);

        return Client.ExecuteWithErrorHandling(request);
    }
}