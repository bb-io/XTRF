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
        public GetCustomersResponse GetCustomers(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/customers", Method.Get, authenticationCredentialsProviders);
            return new GetCustomersResponse()
            {
                SimpleCustomers = client.Get<List<SimpleCustomer>>(request)
            };
        }

        [Action("Get customer details", Description = "Get all information of a specific customer")]
        public Customer GetCustomer(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] int id)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/customers/" + id, Method.Get, authenticationCredentialsProviders);
            return client.Get<Customer>(request);
        }

        [Action("Create customer", Description = "Create a new customer")]
        public SimpleCustomer CreateCustomer(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] CreateCustomer newCustomer)
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
            return client.Post<SimpleCustomer>(request);
        }
    }
}