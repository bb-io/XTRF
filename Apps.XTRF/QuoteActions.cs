﻿using Apps.XTRF.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF
{
    [ActionList]
    public class QuoteActions
    {
        [Action("Get quote details", Description = "Get all information of a specific quote")]
        public Quote GetQuote(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider, [ActionParameter] int id)
        {
            var client = new XtrfClient(url);
            var request = new XtrfRequest("/v2/quotes/" + id, Method.Get, authenticationCredentialsProvider);
            return client.Get<Quote>(request);
        }
    }
}
