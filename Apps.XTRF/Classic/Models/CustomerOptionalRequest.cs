using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Classic.Models
{
    public class CustomerOptionalRequest
    {
        [Display("Customer ID")]
        [DataSource(typeof(CustomerDataHandler))]
        public string? CustomerId { get; set; }
    }
}
