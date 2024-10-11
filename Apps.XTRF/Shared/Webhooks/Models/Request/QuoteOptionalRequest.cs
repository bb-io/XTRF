using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Webhooks.Models.Request;

public class QuoteOptionalRequest
{
    [Display("Quote ID")]
    public string? QuoteId { get; set; }
}