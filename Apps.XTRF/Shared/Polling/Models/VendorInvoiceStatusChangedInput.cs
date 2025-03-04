using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Polling.Models
{
    public class VendorInvoiceStatusChangedInput
    {
        [Display("Vendor invoice ID"), DataSource(typeof(ProviderInvoiceDataHandler))]
        public string? InvoiceId { get; set; }

        [StaticDataSource(typeof(ProviderInvoiceStatusDataHandler))]
        public string? Status { get; set; }
    }
}
