﻿using Apps.XTRF.Responses;
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
    public class QuoteActions
    {
        [Action("Get quote details", Description = "Get all information of a specific quote")]
        public Quote GetQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] int id)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + id, Method.Get, authenticationCredentialsProviders);
            return client.Get<Quote>(request);
        }

        [Action("Create new quote", Description = "Create a new quote")]
        public Quote CreateProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] SimpleQuote quote)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(quote);
            return client.Post<Quote>(request);
        }

        [Action("Get jobs in a quote", Description = "Get all jobs of a specific quote")]
        public GetJobsResponse GetJobsByQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/jobs", Method.Get, authenticationCredentialsProviders);
            return new GetJobsResponse()
            {
                Jobs = client.Get<List<Job>>(request)
            };
        }

        [Action("Get files in a quote", Description = "Get all files of a specific quote")]
        public GetFilesResponse GetFilesByQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/files", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.Get<List<FileXTRF>>(request)
            };
        }
    }
}
