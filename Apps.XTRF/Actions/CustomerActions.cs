using Apps.XTRF.InputParameters;
using Apps.XTRF.Responses;
using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF.Actions
{
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
            [ActionParameter] [Display("Customer ID")] string id,
            [ActionParameter] [Display("Additional fields")] string? embed)
        {
            var endpoint = "/customers/" + id;
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
            request.AddJsonBody(new
            {
                name = newCustomer.Name,
                fullName = newCustomer.FullName,
                contact = new
                {
                    emails = new
                    {
                        primary = newCustomer.Email
                    }
                }
            });
            return client.ExecuteRequest<SimpleCustomer>(request);
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
}