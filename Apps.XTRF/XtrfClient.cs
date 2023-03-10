using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF
{
    public class XtrfClient : RestClient
    {
        public XtrfClient(string url) : base(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = new Uri(url + "/home-api") }) { }
    }
}
