using Apps.XTRF.Utils;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Shared.Models.Responses.Invoices;

public class PaymentResponse
{
    [Display("Amount")]
    public double Amount { get; set; }

    [Display("Dates")]
    public PaymentDateResponse PaymentDate { get; set; } = new();

    [Display("Payment method ID")]
    public string? PaymentMethodId { get; set; }

    public string Notes { get; set; } = string.Empty;
}

public class PaymentDateResponse
{
    [Display("Payment date"), JsonProperty("time")]
    [JsonConverter(typeof(UnixTimeToDateTimeConverter))]
    public DateTime PaymentDate { get; set; }
}