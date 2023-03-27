using Apps.XTRF.InputParameters;
using Apps.XTRF.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF
{
    [ActionList]
    public class CustomerActions
    {
        [Action("Get customers", Description = "Get all customers on this XTRF instance")]
        public GetCustomersResponse GetCustomers(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider)
        {
            var client = new XtrfClient(url);
            var request = new XtrfRequest("/customers", Method.Get, authenticationCredentialsProvider);
            return new GetCustomersResponse()
            {
                SimpleCustomers = client.Get<List<SimpleCustomer>>(request)
            };
        }

        [Action("Get customer details", Description = "Get all information of a specific customer")]
        public Customer GetCustomer(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider, [ActionParameter]int id)
        {
            var client = new XtrfClient(url);
            var request = new XtrfRequest("/customers/" + id, Method.Get, authenticationCredentialsProvider);
            return client.Get<Customer>(request);
        }

        [Action("Create customer", Description = "Create a new customer")]
        public SimpleCustomer CreateCustomer(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider, [ActionParameter] CreateCustomer newCustomer)
        {
            var client = new XtrfClient(url);
            var request = new XtrfRequest("/customers", Method.Post, authenticationCredentialsProvider);
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