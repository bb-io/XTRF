using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Shared.Webhooks.Models.Payloads;

public class QuoteStatusChangedPayload
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("internal id")]
    [Display("Internal ID")]
    public string InternalId { get; set; }

    [JsonProperty("service")]
    public string Service { get; set; }

    [JsonProperty("specialisation")]
    public string Specialisation { get; set; }

    [JsonProperty("client")]
    public string Client { get; set; }

    [JsonProperty("client legal name")]
    [Display("Client legal name")]
    public string ClientLegalName { get; set; }

    [JsonProperty("client price profile")]
    [Display("Client price profile")]
    public string ClientPriceProfile { get; set; }

    [JsonProperty("instructions from client")]
    [Display("Instructions from client")]
    public string InstructionsFromClient { get; set; }

    [JsonProperty("quote manager")]
    [Display("Quote manager")]
    public string QuoteManager { get; set; }

    [JsonProperty("sales person")]
    [Display("Sales person")]
    public string SalesPerson { get; set; }

    [JsonProperty("offer expiry")]
    [Display("Offer expiry")]
    public string OfferExpiry { get; set; }

    [JsonProperty("estimated delivery date and time")]
    [Display("Estimated delivery date and time")]
    public DateTime EstimatedDeliveryDateAndTime { get; set; }

    [JsonProperty("deadline")]
    public DateTime Deadline { get; set; }

    [JsonProperty("created on")]
    [Display("Created on")]
    public DateTime CreatedOn { get; set; }

    [JsonProperty("start date and time")]
    [Display("Start date and time")]
    public DateTime StartDateAndTime { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("previous status")]
    [Display("Previous status")]
    public string PreviousStatus { get; set; }

}