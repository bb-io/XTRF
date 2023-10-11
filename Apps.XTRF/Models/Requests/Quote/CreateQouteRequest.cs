using Blackbird.Applications.Sdk.Utils.Parsers;

namespace Apps.XTRF.Models.Requests.Quote;

public class CreateQouteRequest
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    public int ServiceId { get; set; }
    public int? OpportunityOfferId { get; set; }

    public CreateQouteRequest(CreateQouteInput input)
    {
        Name = input.Name;
        ClientId = IntParser.Parse(input.ClientId, nameof(input.ClientId))!.Value;
        ServiceId = IntParser.Parse(input.ServiceId, nameof(input.ServiceId))!.Value;
        OpportunityOfferId = IntParser.Parse(input.OpportunityOfferId, nameof(input.OpportunityOfferId));
    }
}