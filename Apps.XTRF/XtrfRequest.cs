using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF
{
    internal class XtrfRequest : RestRequest
    {
        public XtrfRequest(string endpoint, Method method, AuthenticationCredentialsProvider authenticationCredentialsProvider) : base(endpoint, method)
        {
            this.AddHeader("X-AUTH-ACCESS-TOKEN", authenticationCredentialsProvider.Value);
            this.AddHeader("accept", "*/*");
        }
    }
}
