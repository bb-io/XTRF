using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Invoice;

public class ProviderInvoiceCreateRequest
{
    [Display("Job ID", Description = "ID of the job for which the invoice should be created")]
    public string JobId { get; set; } = string.Empty;
}