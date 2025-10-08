using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Browser;
using Apps.XTRF.Shared.Models.Requests.User;
using Apps.XTRF.Shared.Models.Responses.Browser;
using Apps.XTRF.Shared.Models.Responses.Provider.Persons;
using Apps.XTRF.Shared.Models.Responses.User;
using Apps.XTRF.Smart.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.Actions
{
    [ActionList("Users")]
    public class UserActions(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
    {

        [Action("Get user details", Description = "Get user details given a user ID")]
        public async Task<GetUserResponse> GetUser([ActionParameter] GetUserRequest request)
        {
            var xtrfRequest = new XtrfRequest($"/users/{request.UserID}", Method.Get, Creds);
            var response = await Client.ExecuteWithErrorHandling<GetUserResponse>(xtrfRequest);

            return response;
        }
    }
}
