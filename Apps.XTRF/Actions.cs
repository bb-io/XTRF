using Apps.XTRF.InputParameters;
using Apps.XTRF.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF
{
    [ActionList]
    public class Actions
    {
        [Action("Get customers", Description = "Get all customers on this XTRF instance")]
        public List<SimpleCustomer>? GetCustomers(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider)
        {
            var client = new XtrfClient(url);
            var request = new XtrfRequest("/customers", Method.Get, authenticationCredentialsProvider);
            return client.Get<List<SimpleCustomer>>(request);
        }

        [Action("Get customer details", Description = "Get all information of a specific customer")]
        public Customer? GetCustomer(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider, [ActionParameter]int id)
        {
            var client = new XtrfClient(url);
            var request = new XtrfRequest("/customers/" + id, Method.Get, authenticationCredentialsProvider);
            return client.Get<Customer>(request);
        }
    }
}