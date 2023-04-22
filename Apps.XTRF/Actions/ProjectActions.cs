using Apps.XTRF.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Actions
{
    [ActionList]
    public class ProjectActions
    {
        [Action("Get project details", Description = "Get all information of a specific project")]
        public Project GetProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] int id)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + id, Method.Get, authenticationCredentialsProviders);
            return client.Get<Project>(request);
        }

        [Action("Create new project", Description = "Create a new project")]
        public Project CreateProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] SimpleProject project)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(project);
            return client.Post<Project>(request);
        }
    }
}
