namespace Apps.XTRF.Shared.Webhooks.Models.Request;

public class SubscribeRequest
{
    public string Url { get; set; }
    public string Event { get; set; }
    public string? Filter { get; set; }
}