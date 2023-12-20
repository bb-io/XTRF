namespace Apps.XTRF.Smart.Models.Requests.Project;

public class CreateProjectRequest
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    public int ServiceId { get; set; }
    public string? ExternalId { get; set; }
    
    public CreateProjectRequest(CreateProjectInput input)
    {
        if (!int.TryParse(input.ClientId, out var clientId))
            throw new("Client ID should be a number");
        
        if (!int.TryParse(input.ServiceId, out var serviceId))
            throw new("Service ID should be a number");
        
        Name = input.Name;
        ClientId = clientId;
        ServiceId = serviceId;
        ExternalId = input.ExternalId;
    }
}