using Apps.XTRF.InputParameters;

namespace Apps.XTRF.Requests;

public class CreateQouteRequest
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    public int ServiceId { get; set; }
    public int OpportunityOfferId { get; set; }

    public CreateQouteRequest(CreateQouteInput input)
    {
        if (!int.TryParse(input.ClientId, out var clientId))
            throw new("Client ID should be a number");
        
        if (!int.TryParse(input.ServiceId, out var serviceId))
            throw new("Service ID should be a number");
        
        if (!int.TryParse(input.OpportunityOfferId, out var opportunityOfferId))
            throw new("Opportunity offer ID should be a number");
        
        Name = input.Name;
        ClientId = clientId;
        ServiceId = serviceId;
        OpportunityOfferId = opportunityOfferId;
    }
}