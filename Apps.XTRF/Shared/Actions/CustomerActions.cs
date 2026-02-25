using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Requests.Customer;
using Apps.XTRF.Shared.Models.Responses.Customer;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Shared.Actions;

[ActionList("Customers")]
public class CustomerActions(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
{
    #region Get

    [Action("Get customers", Description = "Get all customers on this XTRF instance")]
    public async Task<GetCustomersResponse> GetCustomers(
        [ActionParameter] [Display("Updated since")] DateTime? updatedSince)
    {
        var endpoint = "/customers";
        if (updatedSince is not null)
        {
            var timeZoneInfo = await GetTimeZoneInfo();
            var elapsedTimeInMilliseconds = updatedSince.Value.ConvertToUnixTime(timeZoneInfo);
            endpoint += $"?updatedSince={elapsedTimeInMilliseconds}";
        }

        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        var customers = await Client.ExecuteWithErrorHandling<IEnumerable<SimpleCustomer>>(request);
        return new() { SimpleCustomers = customers };
    }
    
    [Action("Get customer details", Description = "Get information about specific customer")]
    public async Task<Customer> GetCustomer([ActionParameter] CustomerIdentifier customerIdentifier)
    {
        customerIdentifier.CustomerId = customerIdentifier.CustomerId?.Trim();
        if (customerIdentifier.CustomerId == null || customerIdentifier.CustomerId == string.Empty)
        {
            throw new PluginMisconfigurationException("Customer ID cannot be empty, please provide a customer ID.");
        }
        var request = new XtrfRequest($"/customers/{customerIdentifier.CustomerId}?embed=persons", Method.Get, Creds);
        var customer = await Client.ExecuteWithErrorHandling<Customer>(request);
        return customer;
    }

    #endregion

    #region Post

    [Action("Create customer", Description = "Create a new customer")]
    public async Task<Customer> CreateCustomer([ActionParameter] CreateCustomerRequest input)
    {

        var request = new XtrfRequest("/customers", Method.Post, Creds)
            .WithJsonBody(new
            {
                name = input.Name,
                fullName = input.FullName,
                notes = input.Notes,
                contact = new
                {
                    phones = input.Phones,
                    emails = new
                    {
                        primary = input.Email,
                        additional = input.AdditionalEmails
                    }
                }
            }, JsonConfig.Settings);

        var customer = await Client.ExecuteWithErrorHandling<Customer>(request);
        return customer;
    }
    
    [Action("Create customer contact", Description = "Create a new contact person for a customer")]
    public async Task<ContactPerson> CreateCustomerContact([ActionParameter] CustomerIdentifier customer,
        [ActionParameter] CreateContactRequest input)
    {
        customer.CustomerId = customer.CustomerId?.Trim();
        if (customer.CustomerId == null || customer.CustomerId == string.Empty)
        {
            throw new PluginMisconfigurationException("Customer ID cannot be empty, please provide a customer ID.");
        }

        var request = new XtrfRequest("/customers/persons", Method.Post, Creds)
            .WithJsonBody(new
            {
                name = input.Name,
                lastName = input.LastName,
                contact = new
                {
                    phones = input.Phones,
                    emails = new
                    {
                        primary = input.Email,
                        additional = input.AdditionalEmails
                    }
                },
                customerId = customer.CustomerId,
                motherTonguesIds = input.MotherTonguesIds
            }, JsonConfig.Settings);
        
        var person = await Client.ExecuteWithErrorHandling<ContactPerson>(request);
        return person;
    }

    #endregion

    #region Put

    [Action("Update customer", Description = "Update a customer, specifying only the fields that require updating")]
    public async Task<Customer> UpdateCustomer([ActionParameter] CustomerIdentifier customerIdentifier,
        [ActionParameter] UpdateCustomerRequest input)
    {
        customerIdentifier.CustomerId = customerIdentifier.CustomerId?.Trim();
        if (customerIdentifier.CustomerId == null || customerIdentifier.CustomerId == string.Empty)
        {
            throw new PluginMisconfigurationException("Customer ID cannot be empty, please provide a customer ID.");
        }
        var getCustomerRequest = 
            new XtrfRequest($"/customers/{customerIdentifier.CustomerId}?embed=persons", Method.Get, Creds);
        var customer = await Client.ExecuteWithErrorHandling<Customer>(getCustomerRequest);
        
        if (input.Name != null || input.FullName != null || input.Notes != null)
        {
            var body = new Dictionary<string, object>
            {
                { "id", customerIdentifier.CustomerId }
            };

            if (input.Name != null)
                body["name"] = input.Name;

            if (input.FullName != null)
                body["fullName"] = input.FullName;

            if (input.Notes != null)
                body["notes"] = input.Notes;

            var updateCustomerRequest = new XtrfRequest($"/customers/{customerIdentifier.CustomerId}", Method.Put, Creds)
                .WithJsonBody(body, JsonConfig.Settings);

            await Client.ExecuteWithErrorHandling(updateCustomerRequest);

            customer.Name = input.Name ?? customer.Name;
            customer.FullName = input.FullName ?? customer.FullName;
            customer.Notes = input.Notes ?? customer.Notes;
        }

        if (input.Email != null || input.AdditionalEmails != null || input.Phones != null)
        {
            var body = new Dictionary<string, object>();

            if (input.Phones != null)
                body["phones"] = input.Phones;

            if (input.Email != null || input.AdditionalEmails != null)
            {
                var emails = new Dictionary<string, object>();

                if (input.Email != null)
                    emails["primary"] = input.Email;

                if (input.AdditionalEmails != null)
                    emails["additional"] = input.AdditionalEmails;

                body["emails"] = emails;
            }

            var updateContactRequest =
                new XtrfRequest($"/customers/{customerIdentifier.CustomerId}/contact", Method.Put, Creds)
                    .WithJsonBody(body, JsonConfig.Settings);

            await Client.ExecuteWithErrorHandling(updateContactRequest);

            if (input.Phones != null) customer.Contact.Phones = input.Phones;
            if (input.Email != null) customer.Contact.Emails.Primary = input.Email;
            if (input.AdditionalEmails != null) customer.Contact.Emails.Additional = input.AdditionalEmails;
        }

        return customer;
    }

    [Action("Update customer contact", Description = "Update a contact person for a customer, specifying only the " +
                                                     "fields that require updating")]
    public async Task<ContactPerson> UpdateCustomerContact([ActionParameter] PersonIdentifier personIdentifier, 
        [ActionParameter] UpdateContactRequest input)
    {
        var getPersonRequest = 
            new XtrfRequest($"/customers/persons/{personIdentifier.PersonId}", Method.Get, Creds);
        var person = await Client.ExecuteWithErrorHandling<ContactPerson>(getPersonRequest);
        
        if (input.Name != null || input.LastName != null || input.MotherTonguesIds != null)
        {
            var updatePersonRequest = new XtrfRequest($"/customers/persons/{personIdentifier.PersonId}", Method.Put, Creds)
                .WithJsonBody(new
                {
                    name = input.Name,
                    lastName = input.LastName,
                    motherTonguesIds = input.MotherTonguesIds
                }, JsonConfig.Settings);

            await Client.ExecuteWithErrorHandling(updatePersonRequest);

            person.Name = input.Name ?? person.Name;
            person.LastName = input.LastName ?? person.LastName;
            person.MotherTonguesIds = input.MotherTonguesIds ?? person.MotherTonguesIds;
        }

        if (input.Email != null || input.AdditionalEmails != null || input.Phones != null)
        {
            var jsonBody = new
            {
                phones = input.Phones ?? person.Contact.Phones,
                emails = new
                {
                    primary = input.Email ?? person.Contact.Emails.Primary,
                    additional = input.AdditionalEmails ?? person.Contact.Emails.Additional
                }
            };

            var updateContactRequest =
                new XtrfRequest($"/customers/persons/{personIdentifier.PersonId}/contact", Method.Put, Creds)
                    .WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateContactRequest);

            person.Contact.Phones = jsonBody.phones;
            person.Contact.Emails.Primary = jsonBody.emails.primary;
            person.Contact.Emails.Additional = jsonBody.emails.additional;
        }

        return person;
    }

    #endregion

    #region Delete

    [Action("Delete customer", Description = "Delete specific customer")]
    public Task DeleteCustomer([ActionParameter] CustomerIdentifier customer)
    {
        customer.CustomerId = customer.CustomerId?.Trim();
        if (customer.CustomerId == null || customer.CustomerId == string.Empty)
        {
            throw new PluginMisconfigurationException("Customer ID cannot be empty, please provide a customer ID.");
        }

        var request = new XtrfRequest($"/customers/{customer.CustomerId}", Method.Delete, Creds);
        return Client.ExecuteWithErrorHandling(request);
    }

    #endregion
    
    public async Task<Person> GetContactPerson(PersonIdentifier personIdentifier)
    {
        var request = new XtrfRequest($"/customers/persons/{personIdentifier.PersonId}", Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<Person>(request);
    }
}